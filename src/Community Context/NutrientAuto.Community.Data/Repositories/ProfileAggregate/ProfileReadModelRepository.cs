using Dapper;
using Microsoft.EntityFrameworkCore;
using NutrientAuto.Community.Data.Context;
using NutrientAuto.Community.Domain.Aggregates.ProfileAggregate;
using NutrientAuto.Community.Domain.ReadModels.ProfileAggregate;
using NutrientAuto.Community.Domain.Repositories.ProfileAggregate;
using NutrientAuto.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Data.Repositories.ProfileAggregate
{
    public class ProfileReadModelRepository : BaseReadModelRepository, IProfileReadModelRepository
    {
        public ProfileReadModelRepository(CommunityDbContext dbContext)
            : base(dbContext)
        {
        }

        public Task<IEnumerable<ProfileListReadModel>> GetProfileListAsync(string nameFilter = null, int pageNumber = 1, int pageSize = 20)
        {
            string sql = @"SELECT Profiles.Id, Profiles.Name, Profiles.Username, Profiles.AvatarImageName, Profiles.AvatarImageUrlPath
                         FROM Profiles
                         WHERE Profiles.Name LIKE '%@nameFilter%'
                         ORDER BY Profiles.Name
                         OFFSET (@pageNumber - 1) * @pageSize ROWS
                         FETCH NEXT @pageSize ROWS ONLY";

            using (DbConnection connection = _dbContext.Database.GetDbConnection())
            {
                return connection
                    .QueryAsync<ProfileListReadModel, Image, ProfileListReadModel>(sql,
                    (profile, avatarImage) =>
                    {
                        profile.AvatarImage = avatarImage;
                        return profile;
                    },
                    new { nameFilter = nameFilter ?? string.Empty, pageNumber, pageSize },
                    splitOn: "AvatarImageName");
            }
        }

        public async Task<ProfileSummaryReadModel> GetProfileSummaryAsync(Guid id)
        {
            string sql = @"SELECT Profiles.Id, Profiles.Genre, Profiles.Name, Profiles.Username, Profiles.Bio, Profiles.BirthDate, Profiles.EmailAddress, Profiles.AvatarImageName, Profiles.AvatarImageUrlPath, Profiles.PrivacyType, 
                         (SELECT COUNT(Friends.FriendId) FROM Friends WHERE Friends.ProfileId = Profiles.Id) AS FriendsCount 
                         FROM Profiles
                         WHERE Profiles.Id = @id";

            using (DbConnection connection = _dbContext.Database.GetDbConnection())
            {
                return (await connection
                    .QueryAsync<ProfileSummaryReadModel, EmailAddress, Image, ProfileSettings, ProfileSummaryReadModel>(sql,
                    (profile, emailAddress, avatarImage, settings) =>
                    {
                        profile.EmailAddress = emailAddress;
                        profile.AvatarImage = avatarImage;
                        profile.PrivacyType = settings.PrivacyType;
                        return profile;
                    },
                    new { id },
                    splitOn: "EmailAddress,AvatarImageName,PrivacyType"))
                    .FirstOrDefault();
            }
        }

        public async Task<ProfileOverviewReadModel> GetProfileOverviewAsync(Guid id)
        {
            string sql = @"SELECT Profiles.Id, Profiles.Genre, Profiles.Name, Profiles.Username, Profiles.Bio, Profiles.BirthDate, Profiles.EmailAddress, Profiles.AvatarImageName, Profiles.AvatarImageUrlPath 
                         FROM Profiles
                         WHERE WHERE Id = @id";

            using (DbConnection connection = _dbContext.Database.GetDbConnection())
            {
                return (await connection
                    .QueryAsync<ProfileOverviewReadModel, EmailAddress, Image, ProfileOverviewReadModel>(sql,
                    (profile, emailAddress, avatarImage) =>
                    {
                        profile.EmailAddress = emailAddress;
                        profile.AvatarImage = avatarImage;
                        return profile;
                    },
                    new { id },
                    splitOn: "EmailAddress,AvatarImageName"))
                    .FirstOrDefault();
            }
        }

        public async Task<ProfileSettingsReadModel> GetProfileSettingsAsync(Guid id)
        {
            string sql = @"SELECT Profiles.Id, Profiles.PrivacyType 
                         FROM Profiles
                         WHERE Id = @id";

            using (DbConnection connection = _dbContext.Database.GetDbConnection())
            {
                return (await connection
                    .QueryAsync<ProfileSettingsReadModel, ProfileSettings, ProfileSettingsReadModel>(sql,
                    (profile, settings) =>
                    {
                        profile.Settings = settings;
                        return profile;
                    },
                    new { id },
                    splitOn: "PrivacyType"))
                    .FirstOrDefault();
            }
        }
    }
}
