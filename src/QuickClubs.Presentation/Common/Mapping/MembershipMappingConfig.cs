using Mapster;
using QuickClubs.Application.Memberships.Common;
using QuickClubs.Application.Memberships.CreateMembership;
using QuickClubs.Contracts.Memberships;

namespace QuickClubs.Presentation.Common.Mapping;
internal class MembershipMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateMembershipRequest, CreateMembershipCommand>(); // All fields line up
        config.NewConfig<MemberResult, MembershipResponse>(); // All fields line up
    }
}
