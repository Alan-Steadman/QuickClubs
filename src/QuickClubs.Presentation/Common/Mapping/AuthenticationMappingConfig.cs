using Mapster;
using QuickClubs.Application.Authentication.Common;
using QuickClubs.Contracts.Authentication;

namespace QuickClubs.Presentation.Common.Mapping;
internal class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AuthenticationResult, AuthenticationResponse>(); // All fields line up
    }
}
