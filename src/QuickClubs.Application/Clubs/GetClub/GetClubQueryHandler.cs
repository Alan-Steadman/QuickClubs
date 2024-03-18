using Dapper;
using QuickClubs.Application.Abstractions.Data;
using QuickClubs.Application.Abstractions.Mediator;
using QuickClubs.Application.Clubs.Common;
using QuickClubs.Domain.Abstractions;
using QuickClubs.Domain.Clubs.Errors;

namespace QuickClubs.Application.Clubs.GetClub;

public sealed class GetClubQueryHandler : IQueryHandler<GetClubQuery, ClubResult>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetClubQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<ClubResult>> Handle(GetClubQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                Id,
                ClubType,
                FullName,
                Acronym,
                Website,
                IsAffiliate
            FROM
                Club
            WHERE
                Id = @ClubId
            """;

        var club = await connection.QueryFirstOrDefaultAsync<ClubResult>(
            sql,
            new
            {
                request.ClubId
            });

        if (club is null)
            return Result.Failure<ClubResult>(ClubErrors.NotFound(request.ClubId));

        return club;
    }
}
