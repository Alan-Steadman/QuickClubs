using Dapper;
using QuickClubs.Application.Abstractions.Data;
using QuickClubs.Application.Abstractions.Mediator;
using QuickClubs.Application.Locations.Common;
using QuickClubs.Domain.Abstractions;

namespace QuickClubs.Application.Locations.GetAllLocations;

public sealed class GetAllLocationsQueryHandler : IQueryHandler<GetAllLocationsQuery, IEnumerable<LocationResult>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetAllLocationsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IEnumerable<LocationResult>>> Handle(GetAllLocationsQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                Id,
                ClubId,
                [Name],
                 /* Building,
                    Locality,
                    Town,
                    County,
                    Postcode, */
                WhatThreeWords,
                OsGridRef,
                --PositionId,
                Directions
                --Version
            FROM
                Location
            WHERE
                ClubId = @ClubId
            ORDER BY
                [Name]
            
            """;

        var locations = await connection.QueryAsync<LocationResult>(
            sql,
            new
            {
                request.ClubId
            });

        return Result.Success<IEnumerable<LocationResult>>(locations);
    }
}