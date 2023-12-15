using Mapster;
using QuickClubs.Application.Clubs.Common;
using QuickClubs.Application.Clubs.CreateClub;
using QuickClubs.Contracts.Clubs;

namespace QuickClubs.Presentation.Common.Mapping;

internal class ClubMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ClubResult, ClubResponse>(); // All fields line up

        config.NewConfig<CreateClubRequest, CreateClubCommand>(); // All fields line up


    }
}
