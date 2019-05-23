using NutrientAuto.Shared.ValueObjects;
using System;
using System.Text;

namespace NutrientAuto.Community.Domain.Aggregates.PostAggregate
{
    public class PostLike : ValueObject<PostLike>
    {
        public Guid ProfileId { get; private set; }
        public DateTime DateCreated { get; private set; }

        protected PostLike()
        {
        }

        public PostLike(Guid profileId)
        {
            ProfileId = profileId;
            DateCreated = DateTime.Now;
        }

        public override string ToString()
        {
            return new StringBuilder()
                .AppendLine($"Id do usuário: {ProfileId}")
                .AppendLine($"Data da criação: {DateCreated}")
                .ToString();
        }
    }
}