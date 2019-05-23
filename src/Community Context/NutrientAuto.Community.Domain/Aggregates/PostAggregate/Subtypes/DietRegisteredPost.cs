using System;

namespace NutrientAuto.Community.Domain.Aggregates.PostAggregate.Subtypes
{
    public class DietRegisteredPost : Post
    {
        protected DietRegisteredPost()
        {
        }

        public DietRegisteredPost(Guid profileId, Guid dietId)
            : base(profileId, "Nova Dieta iniciada", $"Eu iniciei uma nova dieta. Veja mais detalhes no meu registro de dietas.")
        {
            EntityReference = EntityReference.Diet(dietId);
        }
    }
}
