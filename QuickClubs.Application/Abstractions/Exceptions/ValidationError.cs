﻿namespace QuickClubs.Application.Abstractions.Exceptions;

public sealed record ValidationError(string PropertyName, string ErrorMessage);