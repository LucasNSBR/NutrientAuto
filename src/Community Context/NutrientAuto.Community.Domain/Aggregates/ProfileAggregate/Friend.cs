﻿using NutrientAuto.Shared.ValueObjects;
using System;
using System.Text;

namespace NutrientAuto.Community.Domain.Aggregates.ProfileAggregate
{
    public class Friend : ValueObject<Friend>
    {
        public Guid FriendId { get; private set; }

        protected Friend()
        {
        }

        public Friend(Guid friendId)
        {
            FriendId = friendId;
        }

        public override string ToString()
        {
            return new StringBuilder()
                .AppendLine($"Id do amigo: {FriendId}")
                .ToString();
        }
    }
}
