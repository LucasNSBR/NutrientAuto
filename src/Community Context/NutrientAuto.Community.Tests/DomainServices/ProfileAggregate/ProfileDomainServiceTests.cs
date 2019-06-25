using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NutrientAuto.Community.Domain.Aggregates.ProfileAggregate;
using NutrientAuto.Community.Domain.DomainServices.ProfileAggregate;
using NutrientAuto.Community.Domain.Repositories.ProfileAggregate;
using NutrientAuto.Shared.Commands;
using NutrientAuto.Shared.ValueObjects;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Tests.DomainServices.ProfileAggregate
{
    [TestClass]
    public class ProfileDomainServiceTests
    {
        #region Arrange
        private EmailAddress GetEmailAddress()
        {
            EmailAddress emailAddress = new EmailAddress("lucas@hotmail.com");
            return emailAddress;
        }

        private Image GetAvatarImage()
        {
            Image avatarImage = new Image("blobs.azure.com/a1e51afe50e616", "DSC_00021");
            return avatarImage;
        }

        private Profile GetNewProfile(Guid? profileId = null)
        {
            Profile profile = new Profile(profileId ?? Guid.NewGuid(), Genre.Male, GetAvatarImage(), "Lucas Pereira Campos", "lucasnsbr", GetEmailAddress(), DateTime.Now.AddYears(-21));
            return profile;
        }
        #endregion

        #region Profile Access
        [TestMethod]
        public async Task ProfileOwnerShouldBeAllowedToAccessData()
        {
            Mock<IProfileRepository> mock = new Mock<IProfileRepository>();
            Profile profile = GetNewProfile();

            mock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(profile);

            ProfileDomainService profileDomainService = new ProfileDomainService(mock.Object);
            ProfileAccessResult accessResult = await profileDomainService.CanAccessProfileData(profile.Id, profile.Id);

            Assert.AreEqual(ProfileAccessResult.CanAccess, accessResult);
        }

        private IProfileRepository GetSimpleProfileRepository(Profile profileOne, Profile profileTwo)
        {
            Mock<IProfileRepository> mock = new Mock<IProfileRepository>();

            mock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
              .ReturnsAsync((Guid id) =>
              {
                  if (id == profileOne.Id)
                      return profileOne;
                  else
                      return profileTwo;
              });

            return mock.Object;
        }

        [TestMethod]
        public async Task ProfileFriendsShouldBeAllowedToAccess()
        {
            Profile profile = GetNewProfile(Guid.NewGuid());
            Profile profileTwo = GetNewProfile(Guid.NewGuid());

            profileTwo.ChangeSettings(new ProfileSettings(PrivacyType.Protected));

            profile.AddFriend(profileTwo);
            profileTwo.AddFriend(profile);

            ProfileDomainService profileDomainService = new ProfileDomainService(GetSimpleProfileRepository(profile, profileTwo));
            ProfileAccessResult accessResult = await profileDomainService.CanAccessProfileData(profile.Id, profileTwo.Id);

            Assert.AreEqual(ProfileAccessResult.CanAccess, accessResult);
        }

        [TestMethod]
        public async Task ProfileNotFriendsShouldBeNotAllowedToAccess()
        {
            Profile profile = GetNewProfile(Guid.NewGuid());
            Profile profileTwo = GetNewProfile(Guid.NewGuid());
            
            ProfileDomainService profileDomainService = new ProfileDomainService(GetSimpleProfileRepository(profile, profileTwo));
            ProfileAccessResult accessResult = await profileDomainService.CanAccessProfileData(profile.Id, profileTwo.Id);

            Assert.AreEqual(ProfileAccessResult.Forbidden, accessResult);
        }

        [TestMethod]
        public async Task ShouldNotFindProfileInRepository()
        {
            Mock<IProfileRepository> mock = new Mock<IProfileRepository>();
            mock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => null);

            ProfileDomainService profileDomainService = new ProfileDomainService(mock.Object);
            ProfileAccessResult accessResult = await profileDomainService.CanAccessProfileData(Guid.NewGuid(), Guid.NewGuid());

            Assert.AreEqual(ProfileAccessResult.NotFound, accessResult);
        }
        #endregion

        #region Make Friends
        [TestMethod]
        public async Task ShouldMakeUsersFriends()
        {
            Profile profile = GetNewProfile(Guid.NewGuid());
            Profile profileTwo = GetNewProfile(Guid.NewGuid());
            
            ProfileDomainService profileDomainService = new ProfileDomainService(GetSimpleProfileRepository(profile, profileTwo));
            CommandResult commandResult = await profileDomainService.MakeFriends(profile.Id, profileTwo.Id);

            Assert.IsTrue(commandResult.Success);
            Assert.AreEqual(1, profile.Friends.Count);
            Assert.AreEqual(1, profileTwo.Friends.Count);
        }

        [TestMethod]
        public async Task ShouldFailToMakeUsersFriendsTwoTimes()
        {
            Profile profile = GetNewProfile(Guid.NewGuid());
            Profile profileTwo = GetNewProfile(Guid.NewGuid());

            ProfileDomainService profileDomainService = new ProfileDomainService(GetSimpleProfileRepository(profile, profileTwo));
            CommandResult commandResult = await profileDomainService.MakeFriends(profile.Id, profileTwo.Id);

            Assert.IsTrue(commandResult.Success);
            Assert.AreEqual(1, profile.Friends.Count);
            Assert.AreEqual(1, profileTwo.Friends.Count);

            CommandResult secondCommandResult = await profileDomainService.MakeFriends(profile.Id, profileTwo.Id);
            Assert.IsFalse(secondCommandResult.Success);
            Assert.AreEqual("Você e esse usuário já são amigos.", secondCommandResult.Notifications.FirstOrDefault().Description);
        }

        [TestMethod]
        public async Task ShouldFailToFindUserIdsMakeFriends()
        {
            Mock<IProfileRepository> mock = new Mock<IProfileRepository>();
            mock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => null);

            ProfileDomainService profileDomainService = new ProfileDomainService(mock.Object);
            CommandResult commandResult = await profileDomainService.MakeFriends(Guid.NewGuid(), Guid.NewGuid());

            Assert.IsFalse(commandResult.Success);
            Assert.AreEqual("Ocorreu um erro ao buscar os perfis envolvidos na operação.", commandResult.Notifications.FirstOrDefault().Description);
        }

        [TestMethod]
        public async Task ShouldFailToMakeUserFriendWithHimself()
        {
            Profile profile = GetNewProfile();

            Mock<IProfileRepository> mock = new Mock<IProfileRepository>();
            mock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(profile);

            ProfileDomainService profileDomainService = new ProfileDomainService(mock.Object);
            CommandResult commandResult = await profileDomainService.MakeFriends(profile.Id, profile.Id);

            Assert.IsFalse(commandResult.Success);
            Assert.AreEqual("Não é possível tornar-se amigo de si mesmo.", commandResult.Notifications.FirstOrDefault().Description);
        }
        #endregion

        #region End Friends
        [TestMethod]
        public async Task ShouldEndUsersFriendship()
        {
            Profile profile = GetNewProfile(Guid.NewGuid());
            Profile profileTwo = GetNewProfile(Guid.NewGuid());

            ProfileDomainService profileDomainService = new ProfileDomainService(GetSimpleProfileRepository(profile, profileTwo));
            CommandResult friendsResult = await profileDomainService.MakeFriends(profile.Id, profileTwo.Id);

            Assert.IsTrue(friendsResult.Success);
            Assert.AreEqual(1, profile.Friends.Count);
            Assert.AreEqual(1, profileTwo.Friends.Count);

            CommandResult unfriendsResult = await profileDomainService.EndFriendship(profile.Id, profileTwo.Id);

            Assert.IsTrue(unfriendsResult.Success);
            Assert.AreEqual(0, profile.Friends.Count);
            Assert.AreEqual(0, profileTwo.Friends.Count);
        }

        [TestMethod]
        public async Task ShouldFailToFindUserIdsEndFriendship()
        {
            Mock<IProfileRepository> mock = new Mock<IProfileRepository>();
            mock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => null);

            ProfileDomainService profileDomainService = new ProfileDomainService(mock.Object);
            CommandResult commandResult = await profileDomainService.EndFriendship(Guid.NewGuid(), Guid.NewGuid());

            Assert.IsFalse(commandResult.Success);
            Assert.AreEqual("Ocorreu um erro ao buscar os perfis envolvidos na operação.", commandResult.Notifications.FirstOrDefault().Description);
        }

        [TestMethod]
        public async Task ShouldFailToEndUserFriendWithHimself()
        {
            Profile profile = GetNewProfile();

            Mock<IProfileRepository> mock = new Mock<IProfileRepository>();
            mock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(profile);

            ProfileDomainService profileDomainService = new ProfileDomainService(mock.Object);
            CommandResult commandResult = await profileDomainService.EndFriendship(profile.Id, profile.Id);

            Assert.IsFalse(commandResult.Success);
            Assert.AreEqual("Não é possível tornar-se amigo de si mesmo.", commandResult.Notifications.FirstOrDefault().Description);
        }

        [TestMethod]
        public async Task ShouldFailToEndUsersFriendshipTwoTimes()
        {
            Profile profile = GetNewProfile(Guid.NewGuid());
            Profile profileTwo = GetNewProfile(Guid.NewGuid());

            ProfileDomainService profileDomainService = new ProfileDomainService(GetSimpleProfileRepository(profile, profileTwo));
            CommandResult friendsResult = await profileDomainService.MakeFriends(profile.Id, profileTwo.Id);

            Assert.IsTrue(friendsResult.Success);
            Assert.AreEqual(1, profile.Friends.Count);
            Assert.AreEqual(1, profileTwo.Friends.Count);

            CommandResult unfriendsResult = await profileDomainService.EndFriendship(profile.Id, profileTwo.Id);

            Assert.IsTrue(unfriendsResult.Success);
            Assert.AreEqual(0, profile.Friends.Count);
            Assert.AreEqual(0, profileTwo.Friends.Count);

            CommandResult unfriendsResultTwo = await profileDomainService.EndFriendship(profile.Id, profileTwo.Id);
            Assert.IsFalse(unfriendsResultTwo.Success);
            Assert.AreEqual("Você e esse usuário ainda não são amigos.", unfriendsResultTwo.Notifications.FirstOrDefault().Description);
        }

        #endregion
    }
}
