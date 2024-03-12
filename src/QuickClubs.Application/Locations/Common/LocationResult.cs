namespace QuickClubs.Application.Locations.Common;
public sealed record LocationResult(
    Guid Id,
    Guid ClubId,
    string Name,
    //string Address,
    string WhatThreeWords,
    string OsGridRef,
    string Directions);