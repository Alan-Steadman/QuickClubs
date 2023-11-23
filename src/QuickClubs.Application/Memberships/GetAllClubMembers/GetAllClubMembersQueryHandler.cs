using Dapper;
using QuickClubs.Application.Abstractions.Clock;
using QuickClubs.Application.Abstractions.Data;
using QuickClubs.Application.Abstractions.Mediator;
using QuickClubs.Application.Clubs.Common;
using QuickClubs.Application.Memberships.Common;
using QuickClubs.Domain.Abstractions;

namespace QuickClubs.Application.Memberships.GetAllClubMembers;

public sealed class GetAllClubMembersQueryHandler : IQueryHandler<GetAllClubMembersQuery, IEnumerable<MemberResult>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    private readonly IDateTimeProvider _dateTimeProvider;

    public GetAllClubMembersQueryHandler(
        ISqlConnectionFactory sqlConnectionFactory,
        IDateTimeProvider dateTimeProvider)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<IEnumerable<MemberResult>>> Handle(GetAllClubMembersQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        DateTime memberAtDate = request.MemberAtDate ?? _dateTimeProvider.UtcNow;

        const string sql = """
            SELECT
                MemberId = Membership.Id
              , Membership.MembershipNumber
              , Membership.MembershipName
              , Membership.EndDate

              , [Member].UserId
              , [User].FirstName
              , [User].LastName
              , [User].Email

            FROM
            	Membership
            	JOIN [Member] ON [Member].MembershipId = Membership.Id
            	JOIN [User] ON [Member].UserId = [User].Id
            	LEFT JOIN UserProfile ON UserProfile.UserId = [User].Id
            WHERE
            	Membership.ClubId = @ClubId
            	AND Membership.Approval_IsApproved = 1
            	--AND Membership.Paid = 1
            	AND @MemberAtDate BETWEEN Membership.StartDate AND Membership.EndDate
            """;

        var members = await connection.QueryAsync<MemberResult>(
            sql,
            new
            {
                request.ClubId,
                memberAtDate
            });

        return Result.Success<IEnumerable<MemberResult>>(members);
    }
}
