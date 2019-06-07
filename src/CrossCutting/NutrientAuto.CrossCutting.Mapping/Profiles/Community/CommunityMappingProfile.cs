using AutoMapper;
using NutrientAuto.Community.Domain.Aggregates.MeasureAggregate;
using NutrientAuto.Community.Domain.Aggregates.SeedWork;
using NutrientAuto.Community.Domain.Commands.SeedWork;
using NutrientAuto.Shared.ValueObjects;

namespace NutrientAuto.CrossCutting.Mapping.Profiles.Community
{
    public class CommunityMappingProfile : Profile
    {
        public CommunityMappingProfile()
        {
            CreateMap<MacronutrientTableDto, MacronutrientTable>();
            CreateMap<MicronutrientTableDto, MicronutrientTable>();
            CreateMap<ImageDto, Image>();
            CreateMap<MeasureLineDto, MeasureLine>();
        }
    }
}
