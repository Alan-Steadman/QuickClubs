namespace QuickClubs.Contracts.Constants.Permission;
public static class ApplicationClaimTypes
{
    public static string Sub = "sub";                   // == JwtRegisteredClaimNames.Sub
    public static string Email = "email";               // == JwtRegisteredClaimNames.Email
    public static string GivenName = "given_name";      // == JwtRegisteredClaimNames.GivenName
    public static string FamilyName = "family_name";    // == JwtRegisteredClaimNames.FamilyName
    public static string Jti = "jti";                   // == JwtRegisteredClaimNames.Jti

    // TODO: Start using Permissions!
    public static string Permission = "permission";
}
