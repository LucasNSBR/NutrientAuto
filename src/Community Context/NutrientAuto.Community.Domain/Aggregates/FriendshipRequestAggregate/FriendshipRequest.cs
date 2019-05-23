using NutrientAuto.Shared.Entities;
using System;

namespace NutrientAuto.Community.Domain.Aggregates.FriendshipRequestAggregate
{
    public class FriendshipRequest : Entity<FriendshipRequest>, IAggregateRoot
    {
        public Guid RequesterId { get; private set; }
        public Guid RequestedId { get; private set; }

        public DateTime DateCreated { get; private set; }

        public bool IsPending => AcceptedDate == null;
        public bool IsAccepted => AcceptedDate != null;

        public string RequestBody { get; private set; }
        public DateTime? AcceptedDate { get; private set; }

        protected FriendshipRequest()
        {
        }

        public FriendshipRequest(Guid requesterId, Guid requestedId, string requestBody)
        {
            RequesterId = requesterId;
            RequestedId = requestedId;
            DateCreated = DateTime.Now;

            RequestBody = requestBody;
        }

        public bool IsRequester(Guid profileId)
        {
            return profileId == RequesterId;
        }

        public bool IsRequested(Guid profileId)
        {
            return profileId == RequestedId;
        }

        public void Accept()
        {
            if (!IsPending)
                AddNotification("Erro de aceitação", "Só é possível aceitar uma solicitação pendente.");

            AcceptedDate = DateTime.Now;
        }
    }
}
