namespace QuickClubs.Domain.Common;

public record Currency
{
    internal static readonly Currency None = new("", "");
    public static readonly Currency Gbp = new("GBP", "£");
    public static readonly Currency Usd = new("USD", "$");
    public static readonly Currency Eur = new("EUR", "€");

    private Currency(string code, string symbol)
    {
        Code = code;
        Symbol = symbol;
    }

    public string Code { get; init; }
    public string Symbol { get; init; }

    public const int CodeMaxLength = 3;

    public static Currency FromCode(string code)
    {
        return All.FirstOrDefault(c => c.Code == code) ??
               throw new ApplicationException("The currency code is invalid");
    }

    public static readonly IReadOnlyCollection<Currency> All = new[]
    {
        Gbp,
        Usd,
        Eur
    };
}
