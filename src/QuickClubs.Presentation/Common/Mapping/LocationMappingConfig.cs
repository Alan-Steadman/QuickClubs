using Mapster;
using QuickClubs.Application.Locations.Common;
using QuickClubs.Application.Locations.CreateLocation;
using QuickClubs.Contracts.Locations;

namespace QuickClubs.Presentation.Common.Mapping;
internal class LocationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<LocationResult, LocationResponse>();  // All fields line up

        config.NewConfig<(Guid ClubId, CreateLocationRequest Request), CreateLocationCommand>()
            .Map(dest => dest.ClubId, src => src.ClubId)
            .Map(dest => dest, src => src.Request);
    }
}
