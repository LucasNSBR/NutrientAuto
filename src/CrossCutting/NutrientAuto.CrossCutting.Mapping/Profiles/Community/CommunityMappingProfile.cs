using AutoMapper;
using NutrientAuto.Community.Domain.Aggregates.SeedWork;
using NutrientAuto.Community.Domain.Commands.SeedWork;

namespace NutrientAuto.CrossCutting.Mapping.Profiles.Community
{
    public class CommunityMappingProfile : Profile
    {
        public CommunityMappingProfile()
        {
            CreateMap<MacronutrientTableDto, MacronutrientTable>();
            CreateMap<MicronutrientTableDto, MicronutrientTable>();
        }
    }
}
