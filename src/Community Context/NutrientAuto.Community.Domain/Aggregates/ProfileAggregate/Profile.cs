using NutrientAuto.Shared.Entities;
using NutrientAuto.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NutrientAuto.Community.Domain.Aggregates.ProfileAggregate
{
    public class Profile : Entity<Profile>, IAggregateRoot
    {
        public Image AvatarImage { get; private set; }
        public Genre Genre { get; private set; }
        public string Name { get; private set; }
        public string Username { get; private set; }
        public EmailAddress EmailAddress { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string Bio { get; private set; }
        public ProfileSettings Settings { get; private set; }

        public bool IsPublic => Settings.PrivacyType == PrivacyType.Public;
        public bool IsProtected => Settings.PrivacyType == PrivacyType.Protected;
        public bool IsPrivate => Settings.PrivacyType == PrivacyType.Private;

        private readonly List<Friend> _friends;
        public IReadOnlyList<Friend> Friends => _friends;

        protected Profile()
        {
        }

        public Profile(Guid id, Genre genre, string name, string username, EmailAddress emailAddress, DateTime birthDate)
        {
            Id = id;
            Genre = genre;
            Name = name;
            Username = username;
            EmailAddress = emailAddress;
            BirthDate = birthDate;
            Settings = new ProfileSettings(PrivacyType.Public);
            _friends = new List<Friend>();
        }

        public void Update(Genre genre, string name, string username, EmailAddress emailAddress, DateTime birthDate, string bio)
        {
            Genre = genre;
            Name = name;
            Username = username;
            EmailAddress = emailAddress;
            BirthDate = birthDate;
            Bio = bio;
        }

        public void SetAvatarImage(Image avatarImage)
        {
            AvatarImage = avatarImage;
        }

        public void ChangeSettings(ProfileSettings settings)
        {
            Settings = settings;
        }

        public bool IsFriend(Guid otherProfileId) => _friends.Any(f => f.FriendId == otherProfileId);
        public bool IsFriend(Profile otherProfile) => _friends.Any(f => f.FriendId == otherProfile.Id);
        public Friend FindFriend(Guid otherProfileId) => _friends.Find(f => f.FriendId == otherProfileId);

        public void AddFriend(Profile otherProfile)
        {
            if (otherProfile.Id == Id)
                AddNotification("Erro ao adicionar amigo", "Você não pode adicionar a si mesmo.");
            if (IsFriend(otherProfile))
                AddNotification("Erro ao adicionar amigo", "Você e esse usuário já são amigos.");

            _friends.Add(new Friend(otherProfile.Id));
        }

        public void RemoveFriend(Profile otherProfile)
        {
            Friend friend = FindFriend(otherProfile.Id);
            if (friend == null)
            {
                AddNotification("Erro ao cancelar amizade", "Você e esse usuário ainda não são amigos.");
                return;
            }

            _friends.Remove(friend);
        }
    }
}
