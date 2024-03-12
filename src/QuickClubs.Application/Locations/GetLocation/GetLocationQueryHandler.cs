using Dapper;
using QuickClubs.Application.Abstractions.Data;
using QuickClubs.Application.Abstractions.Mediator;
using QuickClubs.Application.Locations.Common;
using QuickClubs.Domain.Abstractions;

namespace QuickClubs.Application.Locations.GetLocation;

public sealed class GetLocationQueryHandler : IQueryHandler<GetLocationQuery, LocationResult>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetLocationQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<LocationResult>> Handle(GetLocationQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                Id,
                ClubId,
                [Name],
                /*Building,
                Locality,
                Town,
                County,
                Postcode,*/
                WhatThreeWords,
                OsGridRef,
                --PositionId,
                Directions
                --Version
            FROM
                Location
            WHERE
                Id = @LocationId
            """;

        var location = await connection.QueryFirstOrDefaultAsync<LocationResult>(
            sql,
            new
            {
                request.LocationId
            });

        return location;
    }
}
