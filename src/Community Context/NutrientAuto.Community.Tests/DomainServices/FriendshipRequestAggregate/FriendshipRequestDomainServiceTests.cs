using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NutrientAuto.Community.Domain.Aggregates.FriendshipRequestAggregate;
using NutrientAuto.Community.Domain.DomainServices.FriendshipRequestAggregate;
using NutrientAuto.Community.Domain.Repositories.FriendshipRequestAggregate;
using NutrientAuto.Shared.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Tests.DomainServices.FriendshipRequestAggregate
{
    [TestClass]
    public class FriendshipRequestDomainServiceTests
    {
        public IFriendshipRequestRepository GetRepository(FriendshipRequest request)
        {
            Mock<IFriendshipRequestRepository> mock = new Mock<IFriendshipRequestRepository>();

            mock.Setup(repo => repo.GetActiveByCompositeIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(request);

            return mock.Object;
        }

        [TestMethod]
        public async Task ShouldDumpFriendship()
        {
            Guid requesterId = Guid.NewGuid();
            Guid requestedId = Guid.NewGuid();

            FriendshipRequest request = new FriendshipRequest(Guid.NewGuid(), Guid.NewGuid(), null);
            request.Accept();

            IFriendshipRequestRepository repository = GetRepository(request);

            IFriendshipRequestDomainService domainService = new FriendshipRequestDomainService(repository);
            CommandResult commandResult = await domainService.DumpExistingFriendshipRequest(requesterId, requestedId);
            FriendshipRequest friendshipRequest = await repository.GetActiveByCompositeIdAsync(requesterId, requestedId);

            Assert.IsTrue(commandResult.Success);
            Assert.AreEqual(FriendshipRequestStatus.Dumped, friendshipRequest.Status);
        }

        [TestMethod]
        public async Task ShouldFailToFindFriendship()
        {
            IFriendshipRequestRepository repository = GetRepository(null);

            IFriendshipRequestDomainService domainService = new FriendshipRequestDomainService(repository);
            CommandResult commandResult = await domainService.DumpExistingFriendshipRequest(Guid.NewGuid(), Guid.NewGuid());

            Assert.IsFalse(commandResult.Success);
            Assert.AreEqual("A solicitação de amizade não foi encontrada no banco de dados.", commandResult.Notifications.FirstOrDefault().Description);
        }

        [TestMethod]
        public async Task ShouldFailToDumpANonAcceptedRequest()
        {
            Guid requesterId = Guid.NewGuid();
            Guid requestedId = Guid.NewGuid();

            FriendshipRequest request = new FriendshipRequest(requesterId, requestedId, null);

            IFriendshipRequestRepository repository = GetRepository(request);

            IFriendshipRequestDomainService domainService = new FriendshipRequestDomainService(repository);
            CommandResult commandResult = await domainService.DumpExistingFriendshipRequest(requesterId, requestedId);
            FriendshipRequest friendshipRequest = await repository.GetActiveByCompositeIdAsync(requesterId, requestedId);

            Assert.IsFalse(commandResult.Success);
            Assert.AreEqual("Só é possível baixar uma solicitação que já foi aceita.", commandResult.Notifications.FirstOrDefault().Description);
        }
    }
}
