using Dapper;
using QuickClubs.Application.Abstractions.Data;
using QuickClubs.Application.Abstractions.Mediator;
using QuickClubs.Application.Clubs.Common;
using QuickClubs.Domain.Abstractions;

namespace QuickClubs.Application.Clubs.GetAllClubs;

public sealed class GetAllClubsQueryHandler : IQueryHandler<GetAllClubsQuery, IEnumerable<ClubResult>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetAllClubsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IEnumerable<ClubResult>>> Handle(GetAllClubsQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                Id,
                FullName,
                Acronym,
                Website,
                IsAffiliate
            FROM
                Club
            ORDER BY
                FullName
            """;

        var clubs = await connection.QueryAsync<ClubResult>(
            sql);

        return Result.Success<IEnumerable<ClubResult>>(clubs) ;
    }
}
