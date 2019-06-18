using Microsoft.VisualStudio.TestTools.UnitTesting;
using NutrientAuto.Community.Domain.Aggregates.ProfileAggregate;
using NutrientAuto.Shared.ValueObjects;
using System;

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

        private Profile GetProfile()
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
            Profile profile = GetProfile();

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
            Profile profile = GetProfile();

            Assert.IsNotNull(profile.Settings);
            Assert.AreEqual(PrivacyType.Private, profile.Settings.PrivacyType);
        }
        #endregion

        #region Update
        [TestMethod]
        public void ShouldUpdateProfileAggregate()
        {
            Profile profile = GetProfile();

            profile.Update(Genre.Male, "Pedro", "Pedronsbr", DateTime.Now.AddYears(-50), "Colocando uma nova bio");

            Assert.AreEqual(Genre.Male, profile.Genre);
            Assert.AreEqual("Pedro", profile.Name);
            Assert.AreEqual("Pedronsbr", profile.Username);
            Assert.AreEqual(DateTime.Now.AddYears(-50).Year, profile.BirthDate.Year);
            Assert.AreEqual("Colocando uma nova bio", profile.Bio);
        }
        #endregion
    }
}
