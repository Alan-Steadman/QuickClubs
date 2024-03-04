using QuickClubs.Domain.Common;

namespace QuickClubs.Domain.Locations.ValueObjects;
public sealed record Position(
    Address? Address,
    WhatThreeWords? WhatThreeWords,
    OsGridRef? OsGridRef);
