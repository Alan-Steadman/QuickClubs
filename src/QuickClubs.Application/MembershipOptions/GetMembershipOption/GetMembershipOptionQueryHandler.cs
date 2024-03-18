using Dapper;
using QuickClubs.Application.Abstractions.Data;
using QuickClubs.Application.Abstractions.Mediator;
using QuickClubs.Application.MembershipOptions.Common;
using QuickClubs.Domain.Abstractions;
using QuickClubs.Domain.MembershipOptions.Errors;

namespace QuickClubs.Application.MembershipOptions.GetMembershipOption;

public sealed class GetMembershipOptionQueryHandler : IQueryHandler<GetMembershipOptionQuery, MembershipOptionResult>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetMembershipOptionQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<MembershipOptionResult>> Handle(GetMembershipOptionQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        var membershipOptionInitial = await connection.QueryFirstOrDefaultAsync<MembershipOptionResult> (
            """
            SELECT
                Id,
                ClubId,
                [Name],
                [Period],
                HasCutoff = CAST(CASE WHEN (CutoffMonth IS NULL AND CutoffDay IS NULL) THEN 0 ELSE 1 END AS BIT),
                CutoffMonth,
                CutoffDay
            FROM
                MembershipOption
            WHERE
                Id = @Id
            """,
            new
            {
                request.Id
            });

        if (membershipOptionInitial is null)
            return Result.Failure<MembershipOptionResult>(MembershipOptionErrors.NotFound(request.Id));

        var membershipLevels = await connection.QueryAsync<MembershipLevelResult>(
            """
            SELECT
                Id AS MembershipLevelId,
            	MembershipOptionId,
                [Name],
                [Description],
                MaxMembers,
                MinAge,
                MaxAge,
                PriceAmount
            FROM
                MembershipLevel
            WHERE
                MembershipOptionId = @Id
            """,
            new
            {
                request.Id
            });

        membershipOptionInitial.Levels = membershipLevels.ToList();

        return membershipOptionInitial;

        //var membershipOption = new MembershipOptionResult(
        //membershipOptionInitial.Id,
        //membershipOptionInitial.ClubId,
        //membershipOptionInitial.Name,
        //membershipOptionInitial.Period,
        //membershipOptionInitial.HasCutoff,
        //membershipOptionInitial.CutoffMonth,
        //membershipOptionInitial.CutoffDay,
        //membershipLevels.ToList());

        //return membershipOption;
    }
}
