using Dapper;
using QuickClubs.Application.Abstractions.Data;
using QuickClubs.Application.Abstractions.Mediator;
using QuickClubs.Application.Memberships.Common;
using QuickClubs.Domain.Abstractions;
using QuickClubs.Domain.Memberships.Errors;

namespace QuickClubs.Application.Memberships.GetMembership;

public sealed class GetMembershipQueryHandler : IQueryHandler<GetMembershipQuery, MembershipResult>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetMembershipQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<MembershipResult>> Handle(GetMembershipQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string membershipSql = """
            SELECT
                M.Id,
                M.ClubId,
                M.MembershipOptionId,
                M.MembershipLevelId,
                M.StartDate,
                M.EndDate,
                M.MembershipNumber,
                M.MembershipName,
                M.PriceAmount,
                M.PriceCurrency,
                PriceFormatted = PriceCurrency + CAST(PriceAmount AS NVARCHAR(10)),
                M.Paid,

                M.IsApproved,
                M.ApprovalStatus,
                M.ApprovedBy,
                ApprovedByName = UAB.FirstName + ' ' + UAB.LastName,
                M.ApprovedDate,
                M.ApprovalReason,

                MBR.Id AS MemberID,
                MBR.UserId,
                UM.FirstName,
                UM.LastName
            FROM
                Membership M
                JOIN Member MBR ON MBR.MembershipId = M.Id
            	LEFT JOIN [User] UAB ON M.ApprovedBy = UAB.Id
            	LEFT JOIN [User] UM ON MBR.UserId = UM.Id
            WHERE
                M.Id = @Id
            """;

        Dictionary<Guid, MembershipResult> membershipDictionary = new();
        var memberships = await connection.QueryAsync<MembershipResult, ApprovalResult, MembershipMemberResult, MembershipResult>(
            membershipSql,
            (membership, approval, member) =>
            {
                if (membershipDictionary.TryGetValue(membership.Id, out var existingMembership))
                {
                    membership = existingMembership;
                }
                else
                {
                    membership.Approval = approval;
                    membershipDictionary.Add(membership.Id, membership);
                }

                membership.Members.Add(member);
                return membership;
            },
            new
            {
                request.Id
            }
            , splitOn: "IsApproved, MemberID");

        if (memberships.Count() == 0)
            return Result.Failure<MembershipResult>(MembershipErrors.NotFound(request.Id));

        var membership = membershipDictionary[request.Id];

        return membership;
    }
}
