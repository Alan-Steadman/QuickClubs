﻿using QuickClubs.Application.Abstractions.Mediator;
using QuickClubs.Application.Locations.Common;

namespace QuickClubs.Application.Locations.CreateLocation;
public sealed record CreateLocationCommand(
    Guid ClubId,
    string Name,
    string? AddressBuilding,
    string? AddressStreet,
    string? AddressLocality,
    string? AddressTown,
    string? AddressCounty,
    string? AddressPostcode,
    string? WhatThreeWords,
    string? OsGridRef,
    string? Directions)
    : ICommand<LocationResult>;