namespace QuickClubs.Contracts.Constants.Routes;
public static class ClubsEndpoints
{
    public static string GetAllClubsEndpoint = "clubs";

    public static string GetClubEndpoint(Guid id) => $"clubs/{id}";

    public static string CreateClubEndpoint = "clubs";
}
