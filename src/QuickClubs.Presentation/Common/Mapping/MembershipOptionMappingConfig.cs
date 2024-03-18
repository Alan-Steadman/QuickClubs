using Mapster;
using QuickClubs.Application.MembershipOptions.Common;
using QuickClubs.Application.MembershipOptions.CreateMembershipOption;
using QuickClubs.Contracts.MembershipOptions;

namespace QuickClubs.Presentation.Common.Mapping;

internal sealed class MembershipOptionMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(Guid ClubId, CreateMembershipOptionRequest Request), CreateMembershipOptionCommand>()
            .Map(dest => dest.ClubId, src => src.ClubId)
            .Map(dest => dest, src => src.Request);

        config.NewConfig<MembershipOptionResult, MembershipOptionResponse>(); // All fields line up
    }
}
