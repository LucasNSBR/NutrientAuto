using Microsoft.VisualStudio.TestTools.UnitTesting;
using NutrientAuto.Community.Domain.Aggregates.FriendshipRequestAggregate;
using System;
using System.Linq;

namespace NutrientAuto.Community.Tests.Aggregates.FriendshipRequestAggregate
{
    [TestClass]
    public class FriendshipRequestTests
    {
        private Guid _requesterId;
        private Guid _requestedId;

        public FriendshipRequestTests()
        {
            _requesterId = Guid.NewGuid();
            _requestedId = Guid.NewGuid();
        }

        private FriendshipRequest GetFriendshipRequest()
        {
            return new FriendshipRequest(_requesterId, _requestedId, null);
        }

        #region Initialization
        [TestMethod]
        public void ShouldBePendingAfterInitialization()
        {
            FriendshipRequest friendshipRequest = GetFriendshipRequest();

            Assert.IsTrue(friendshipRequest.IsPending);
        }
        #endregion

        #region Accept
        [TestMethod]
        public void ShouldAcceptFriendshipRequest()
        {
            FriendshipRequest friendshipRequest = GetFriendshipRequest();

            friendshipRequest.Accept();

            Assert.IsTrue(friendshipRequest.IsValid);
            Assert.IsTrue(friendshipRequest.IsAccepted);
        }

        [TestMethod]
        public void ShouldFailToAcceptNonPendingFriendshipRequest()
        {
            FriendshipRequest friendshipRequest = GetFriendshipRequest();

            friendshipRequest.Accept();
            friendshipRequest.Accept();

            Assert.IsFalse(friendshipRequest.IsValid);
            Assert.AreEqual("Só é possível aceitar uma solicitação pendente.", friendshipRequest.GetNotifications().FirstOrDefault().Description);
        }
        #endregion

        #region Reject
        [TestMethod]
        public void ShouldRejectFriendshipRequest()
        {
            FriendshipRequest friendshipRequest = GetFriendshipRequest();

            friendshipRequest.Reject();

            Assert.IsTrue(friendshipRequest.IsValid);
            Assert.IsTrue(friendshipRequest.IsRejected);
        }

        [TestMethod]
        public void ShouldFailToRejectNonPendingFriendshipRequest()
        {
            FriendshipRequest friendshipRequest = GetFriendshipRequest();

            friendshipRequest.Reject();
            friendshipRequest.Reject();

            Assert.IsFalse(friendshipRequest.IsValid);
            Assert.AreEqual("Só é possível rejeitar uma solicitação pendente.", friendshipRequest.GetNotifications().FirstOrDefault().Description);
        }
        #endregion

        #region Cancel
        [TestMethod]
        public void ShouldCancelFriendshipRequest()
        {
            FriendshipRequest friendshipRequest = GetFriendshipRequest();

            friendshipRequest.Cancel();

            Assert.IsTrue(friendshipRequest.IsValid);
            Assert.IsTrue(friendshipRequest.IsCanceled);
        }

        [TestMethod]
        public void ShouldFailToCancelNonPendingFriendshipRequest()
        {
            FriendshipRequest friendshipRequest = GetFriendshipRequest();

            friendshipRequest.Cancel();
            friendshipRequest.Cancel();

            Assert.IsFalse(friendshipRequest.IsValid);
            Assert.AreEqual("Só é possível cancelar uma solicitação pendente.", friendshipRequest.GetNotifications().FirstOrDefault().Description);
        }
        #endregion

        #region Dump
        [TestMethod]
        public void ShouldDumpFriendshipRequest()
        {
            FriendshipRequest friendshipRequest = GetFriendshipRequest();

            friendshipRequest.Accept();
            friendshipRequest.Dump();

            Assert.IsTrue(friendshipRequest.IsValid);
            Assert.IsTrue(friendshipRequest.IsDumped);
        }

        [TestMethod]
        public void ShouldFailToDumpANonAcceptedFriendshipRequest()
        {
            FriendshipRequest friendshipRequest = GetFriendshipRequest();
            
            friendshipRequest.Dump();

            Assert.IsFalse(friendshipRequest.IsValid);
            Assert.AreEqual("Só é possível baixar uma solicitação que já foi aceita.", friendshipRequest.GetNotifications().FirstOrDefault().Description);
        }
        #endregion
    }
}
