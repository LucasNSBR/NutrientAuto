using NutrientAuto.Community.Domain.Aggregates.PostAggregate;
using System;

namespace NutrientAuto.Community.Domain.Factories.PostAggregate
{
    public interface IPostFactory
    {
        Post CreateGoalRegisteredPost(Guid profileId, Guid goalId);
        Post CreateGoalCompletedPost(Guid profileId, Guid measureId);
        Post CreateMeasureRegisteredPost(Guid profileId, Guid measureId);
        Post CreateDietRegisteredPost(Guid profileId, Guid dietId);
        Post CreateProfileUpdatedPost(Guid profileId);
    }
}
