using NutrientAuto.Shared.ValueObjects;
using System;
using System.Text;

namespace NutrientAuto.Community.Domain.Aggregates.PostAggregate
{
    public class EntityReference : ValueObject<EntityReference>
    {
        public bool HasReference { get; private set; }
        public Guid? ReferenceId { get; private set; }
        public EntityReferenceType? ReferenceType { get; private set; }

        protected EntityReference()
        {
        }

        private EntityReference(bool hasReference, Guid? referenceId = null, EntityReferenceType? referenceType = null)
        {
            HasReference = hasReference;
            ReferenceId = referenceId;
            ReferenceType = referenceType;
        }

        public static EntityReference None() => new EntityReference(false);
        public static EntityReference Profile(Guid profileId) => new EntityReference(true, profileId, EntityReferenceType.Profile);
        public static EntityReference Goal(Guid goalId) => new EntityReference(true, goalId, EntityReferenceType.Goal);
        public static EntityReference Measure(Guid measureId) => new EntityReference(true, measureId, EntityReferenceType.Measure);
        public static EntityReference Diet(Guid dietId) => new EntityReference(true, dietId, EntityReferenceType.Diet);

        public override string ToString()
        {
            return new StringBuilder()
                .AppendLine($"Tem referêcia: {HasReference}")
                .AppendLine($"Id do objeto: {ReferenceId.ToString() ?? "Nenhuma"}")
                .AppendLine($"Objeto de Referência: {ReferenceType.ToString() ?? "Nenhuma"}")
                .ToString();
        }
    }
}
