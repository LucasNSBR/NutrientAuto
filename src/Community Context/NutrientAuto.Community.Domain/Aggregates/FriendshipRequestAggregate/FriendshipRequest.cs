using NutrientAuto.Shared.Entities;
using System;

namespace NutrientAuto.Community.Domain.Aggregates.FriendshipRequestAggregate
{
    public class FriendshipRequest : Entity<FriendshipRequest>, IAggregateRoot
    {
        public FriendshipRequestStatus Status { get; private set; }

        public Guid RequesterId { get; private set; }
        public Guid RequestedId { get; private set; }

        public DateTime DateCreated { get; private set; }

        public bool IsPending => Status == FriendshipRequestStatus.Pending;
        public bool IsAccepted => Status == FriendshipRequestStatus.Accepted;
        public bool IsRejected => Status == FriendshipRequestStatus.Rejected;
        public bool IsCanceled => Status == FriendshipRequestStatus.Canceled;
        public bool IsDumped => Status == FriendshipRequestStatus.Dumped;

        public string RequestBody { get; private set; }
        public DateTime DateModified { get; private set; }

        protected FriendshipRequest()
        {
        }

        public FriendshipRequest(Guid requesterId, Guid requestedId, string requestBody)
        {
            Status = FriendshipRequestStatus.Pending;
            RequesterId = requesterId;
            RequestedId = requestedId;
            DateCreated = DateTime.Now;

            RequestBody = requestBody;
            UpdateDateModified();
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

            Status = FriendshipRequestStatus.Accepted;
            UpdateDateModified();
        }

        public void Reject()
        {
            if (!IsPending)
                AddNotification("Erro de rejeição", "Só é possível rejeitar uma solicitação pendente.");

            Status = FriendshipRequestStatus.Rejected;
            UpdateDateModified();
        }

        public void Cancel()
        {
            if (!IsPending)
                AddNotification("Erro de cancelamento", "Só é possível cancelar uma solicitação pendente.");

            Status = FriendshipRequestStatus.Canceled;
            UpdateDateModified();
        }

        public void Dump()
        {
            Status = FriendshipRequestStatus.Dumped;
            UpdateDateModified();
        }

        private void UpdateDateModified()
        {
            DateModified = DateTime.Now;
        }
    }
}
