using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NutrientAuto.Shared.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static PropertyBuilder HasPrecision(this PropertyBuilder propertyBuilder, int preComma, int postComma)
        {
            return propertyBuilder
                .HasColumnType($"decimal({preComma},{postComma})");
        }
    }
}
