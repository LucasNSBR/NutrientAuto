using NutrientAuto.Shared.ValueObjects;
using System.Text;

namespace NutrientAuto.Community.Domain.Aggregates.ProfileAggregate
{
    public class ProfileSettings : ValueObject<ProfileSettings>
    {
        public PrivacyType PrivacyType { get; private set; }

        protected ProfileSettings()
        {
        }

        public ProfileSettings(PrivacyType privacyType)
        {
            PrivacyType = privacyType;
        }

        public override string ToString()
        {
            return new StringBuilder()
                .AppendLine($"Configuração de privacidade: {PrivacyType.ToString()}")
                .ToString();
        }
    }
}
