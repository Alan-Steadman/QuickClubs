namespace QuickClubs.Domain.ClubTypes;
public sealed record ClubType
{
    public static readonly ClubType MotorClub = new("MC", "Motor Club");

    public static readonly IReadOnlyCollection<ClubType> All = new[] 
    {
        MotorClub
    };

    public string Code { get; init; }
    public string Name { get; init; }

    public const int CodeMaxLength = 8;

    public static ClubType? FromCode(string code)
    {
        return All.FirstOrDefault(ct => ct.Code == code);
    }

    private ClubType(string code, string name)
    {
        Code = code;
        Name = name;
    }


}
