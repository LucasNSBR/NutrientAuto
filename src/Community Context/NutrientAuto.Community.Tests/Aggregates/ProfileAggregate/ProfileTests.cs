using Microsoft.VisualStudio.TestTools.UnitTesting;
using NutrientAuto.Community.Domain.Aggregates.ProfileAggregate;
using NutrientAuto.Shared.ValueObjects;
using System;
using System.Linq;

namespace NutrientAuto.Community.Tests.Aggregates.ProfileAggregate
{
    [TestClass]
    public class ProfileTests
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

        private Profile GetNewProfile()
        {
            Guid profileId = Guid.NewGuid();
            Profile profile = new Profile(profileId, Genre.Male, GetAvatarImage(), "Lucas Pereira Campos", "lucasnsbr", GetEmailAddress(), DateTime.Now.AddYears(-21));
            return profile;
        }
        #endregion

        #region Initialization
        [TestMethod]
        public void ShouldSetInitialVariables()
        {
            Profile profile = GetNewProfile();

            Assert.AreEqual(Genre.Male, profile.Genre);
            Assert.AreEqual(GetAvatarImage(), profile.AvatarImage);
            Assert.AreEqual("Lucas Pereira Campos", profile.Name);
            Assert.AreEqual("lucasnsbr", profile.Username);
            Assert.AreEqual(GetEmailAddress(), profile.EmailAddress);
            Assert.AreEqual(DateTime.Now.AddYears(-21).Year, profile.BirthDate.Year);
        }

        [TestMethod]
        public void ShouldInitializeDefaultPrivacySettings()
        {
            Profile profile = GetNewProfile();

            Assert.IsNotNull(profile.Settings);
            Assert.AreEqual(PrivacyType.Private, profile.Settings.PrivacyType);
        }
        #endregion

        #region Update
        [TestMethod]
        public void ShouldUpdateProfileAggregate()
        {
            Profile profile = GetNewProfile();

            profile.Update(Genre.Male, "Pedro", "Pedronsbr", DateTime.Now.AddYears(-50), "Colocando uma nova bio");

            Assert.AreEqual(Genre.Male, profile.Genre);
            Assert.AreEqual("Pedro", profile.Name);
            Assert.AreEqual("Pedronsbr", profile.Username);
            Assert.AreEqual(DateTime.Now.AddYears(-50).Year, profile.BirthDate.Year);
            Assert.AreEqual("Colocando uma nova bio", profile.Bio);
        }

        [TestMethod]
        public void ShouldChangeAvatarImage()
        {
            Profile profile = GetNewProfile();
            Image newAvatarImage = new Image("s3.aws.storage/150", "IMG015100");

            profile.SetAvatarImage(newAvatarImage);

            Assert.AreEqual("s3.aws.storage/150", profile.AvatarImage.UrlPath);
            Assert.AreEqual("IMG015100", profile.AvatarImage.ImageName);
        }

        [TestMethod]
        public void ShouldChangeProfileSettings()
        {
            Profile profile = GetNewProfile();
            ProfileSettings settings = new ProfileSettings(PrivacyType.Public);

            profile.ChangeSettings(settings);

            Assert.IsNotNull(profile.Settings);
            Assert.AreEqual(PrivacyType.Public, profile.Settings.PrivacyType);
        }
        #endregion

        #region Friends 
        [TestMethod]
        public void ShouldAddFriend()
        {
            Profile profile = GetNewProfile();
            Profile profileTwo = GetNewProfile();

            profile.AddFriend(profileTwo);

            Assert.IsTrue(profile.IsValid);
            Assert.AreEqual(1, profile.Friends.Count);
        }
        
        [TestMethod]
        public void ShouldFailToAddFriendSameProfile()
        {
            Profile profile = GetNewProfile();
            
            profile.AddFriend(profile);

            Assert.IsFalse(profile.IsValid);
            Assert.AreEqual("Você não pode adicionar a si mesmo.", profile.GetNotifications().FirstOrDefault().Description);
        }

        [TestMethod]
        public void ShouldFailToAddFriendAlreadyFriends()
        {
            Profile profile = GetNewProfile();
            Profile profileTwo = GetNewProfile();

            profile.AddFriend(profileTwo);
            profile.AddFriend(profileTwo);

            Assert.IsFalse(profile.IsValid);
            Assert.AreEqual("Você e esse usuário já são amigos.", profile.GetNotifications().FirstOrDefault().Description);
        }

        [TestMethod]
        public void ShouldCancelFriendship()
        {
            Profile profile = GetNewProfile();
            Profile profileTwo = GetNewProfile();

            profile.AddFriend(profileTwo);

            Assert.AreEqual(1, profile.Friends.Count);

            profile.RemoveFriend(profileTwo);

            Assert.IsTrue(profile.IsValid);
            Assert.AreEqual(0, profile.Friends.Count);
        }

        [TestMethod]
        public void ShouldFailToCancelFriendshipNotFriends()
        {
            Profile profile = GetNewProfile();
            Profile profileTwo = GetNewProfile();

            profile.RemoveFriend(profileTwo);

            Assert.IsFalse(profile.IsValid);
            Assert.AreEqual("Você e esse usuário ainda não são amigos.", profile.GetNotifications().FirstOrDefault().Description);
        }
        #endregion
    }
}
