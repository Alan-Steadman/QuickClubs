namespace QuickClubs.Contracts.Locations;

public sealed record CreateLocationRequest(
    string Name,
    string? AddressBuilding,
    string? AddressStreet,
    string? AddressLocality,
    string? AddressTown,
    string? AddressCounty,
    string? AddressPostcode,
    string? WhatThreeWords,
    string? OsGridRef,
    string? Directions);
