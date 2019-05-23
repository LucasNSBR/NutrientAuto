using System;

namespace NutrientAuto.Community.Domain.Aggregates.PostAggregate.Subtypes
{
    public class ProfileUpdatedPost : Post
    {
        protected ProfileUpdatedPost()
        {
        }

        public ProfileUpdatedPost(Guid profileId)
            : base(profileId, "Perfil atualizado", $"Atualizei as informações do meu perfil. Veja mais detalhes no sumário do meu perfil.")
        {
            EntityReference = EntityReference.Profile(profileId);
        }
    }
}
