namespace QuickClubs.Domain.Locations.ValueObjects;

public sealed record WhatThreeWords(string Value)
{
    public const int MaxLength = 40; // ///what.three.words has no character limit.  Words are separated with a dot.  What3Words start with a triple slash.
}
