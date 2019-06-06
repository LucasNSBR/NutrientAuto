using NutrientAuto.Community.Domain.Aggregates.PostAggregate;
using System;

namespace NutrientAuto.Community.Domain.Factories.PostAggregate
{
    public class PostFactory : IPostFactory
    {
        public Post CreateGoalRegisteredPost(Guid profileId, Guid goalId)
        {
            EntityReference entityReference = EntityReference.Goal(goalId);

            return new Post(profileId, "Novo objetivo definido", $"Você pode ver mais detalhes no meu registro de objetivos.", entityReference);
        }

        public Post CreateGoalCompletedPost(Guid profileId, Guid measureId)
        {
            EntityReference entityReference = EntityReference.Measure(measureId);

            return new Post(profileId, "Objetivo completado", $"Eu completei um objetivo. Veja mais detalhes no meu registro de objetivos.", entityReference);
        }

        public Post CreateMeasureRegisteredPost(Guid profileId, Guid measureId)
        {
            EntityReference entityReference = EntityReference.Measure(measureId);

            return new Post(profileId, "Nova medição realizada", $"Você pode ver mais detalhes no meu registro de medições.", entityReference);
        }

        public Post CreateDietRegisteredPost(Guid profileId, Guid dietId)
        {
            EntityReference entityReference = EntityReference.Diet(dietId);

            return new Post(profileId, "Nova Dieta iniciada", $"Eu iniciei uma nova dieta. Veja mais detalhes no meu registro de dietas.", entityReference);
        }

        public Post CreateProfileUpdatedPost(Guid profileId)
        {
            EntityReference entityReference = EntityReference.Profile(profileId);

            return new Post(profileId, "Perfil atualizado", $"Atualizei as informações do meu perfil. Veja mais detalhes no sumário do meu perfil.", entityReference);
        }
    }
}
