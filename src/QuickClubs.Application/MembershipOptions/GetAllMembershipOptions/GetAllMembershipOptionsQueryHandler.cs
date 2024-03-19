using Dapper;
using QuickClubs.Application.Abstractions.Data;
using QuickClubs.Application.Abstractions.Mediator;
using QuickClubs.Application.MembershipOptions.Common;
using QuickClubs.Domain.Abstractions;
using QuickClubs.Domain.Clubs.ValueObjects;

namespace QuickClubs.Application.MembershipOptions.GetAllMembershipOptions;

public sealed class GetAllMembershipOptionsQueryHandler : IQueryHandler<GetAllMembershipOptionsQuery, IEnumerable<MembershipOptionResult>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetAllMembershipOptionsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IEnumerable<MembershipOptionResult>>> Handle(GetAllMembershipOptionsQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
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
                ClubId = @ClubId
            ORDER BY
                [Name]
            """;


        var membershipOptions = await connection.QueryAsync<MembershipOptionResult>(
            sql,
            new {
                request.ClubId
            });


        var membershipLevels = await connection.QueryAsync<MembershipLevelResult>(
            """
            SELECT
                ML.Id AS MembershipLevelId,
            	ML.MembershipOptionId,
                ML.[Name],
                ML.[Description],
                ML.MaxMembers,
                ML.MinAge,
                ML.MaxAge,
                ML.PriceAmount
            FROM
                MembershipLevel ML
                JOIN MembershipOption MO ON ML.MembershipOptionId = MO.Id
            WHERE
                MO.ClubId = @ClubId
            """,
            new
            {
                request.ClubId
            });

        foreach (var membershipOption in membershipOptions)
        {
            membershipOption.Levels = membershipLevels.Where(x => x.MembershipOptionId == membershipOption.Id).ToList();
        }

        return Result.Success<IEnumerable<MembershipOptionResult>>(membershipOptions);
    }
}
