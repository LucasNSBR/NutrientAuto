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
using System.Data.SqlClient;
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

        public async Task<IEnumerable<ProfileListReadModel>> GetProfileListAsync(string nameFilter = null, int pageNumber = 1, int pageSize = 20)
        {
            string sql = $@"SELECT Profiles.Id, Profiles.Name, Profiles.Username, 
                         Profiles.AvatarImageName AS ImageName, Profiles.AvatarImageUrlPath as UrlPath
                         FROM Profiles
                         WHERE Profiles.Name LIKE '%{@nameFilter ?? string.Empty}%'
                         ORDER BY Profiles.Name
                         OFFSET (@pageNumber - 1) * @pageSize ROWS
                         FETCH NEXT @pageSize ROWS ONLY";

            using (DbConnection connection = new SqlConnection(_dbContext.Database.GetDbConnection().ConnectionString))
            {
                return await connection
                    .QueryAsync<ProfileListReadModel, Image, ProfileListReadModel>(sql,
                    (profile, avatarImage) =>
                    {
                        profile.AvatarImage = avatarImage;
                        return profile;
                    },
                    new { nameFilter = nameFilter ?? string.Empty, pageNumber, pageSize },
                    splitOn: "ImageName");
            }
        }

        public async Task<ProfileSummaryReadModel> GetProfileSummaryAsync(Guid id)
        {
            string sql = @"SELECT Profiles.Id, Profiles.Genre, Profiles.Name, Profiles.Username, Profiles.Bio, Profiles.BirthDate, Profiles.PrivacyType, 
                         (SELECT COUNT(Friends.Id) FROM Friends WHERE Profiles.Id = Friends.FriendId) AS FriendsCount,
                         Profiles.EmailAddress AS Email, 
                         Profiles.AvatarImageName AS ImageName, Profiles.AvatarImageUrlPath AS UrlPath
                         FROM Profiles
                         WHERE Profiles.Id = @id";

            using (DbConnection connection = new SqlConnection(_dbContext.Database.GetDbConnection().ConnectionString))
            {
                return (await connection
                    .QueryAsync<ProfileSummaryReadModel, EmailAddress, Image, ProfileSummaryReadModel>(sql,
                    (profile, emailAddress, avatarImage) =>
                    {
                        profile.EmailAddress = emailAddress;
                        profile.AvatarImage = avatarImage;
                        return profile;
                    },
                    new { id },
                    splitOn: "Email,ImageName"))
                    .FirstOrDefault();
            }
        }

        public async Task<ProfileOverviewReadModel> GetProfileOverviewAsync(Guid id)
        {
            string sql = @"SELECT Profiles.Id, Profiles.Genre, Profiles.Name, Profiles.Username, Profiles.Bio, Profiles.BirthDate, 
                         Profiles.EmailAddress AS Email, 
                         Profiles.AvatarImageName AS ImageName, Profiles.AvatarImageUrlPath AS UrlPath
                         FROM Profiles
                         WHERE WHERE Profiles.Id = @id";

            using (DbConnection connection = new SqlConnection(_dbContext.Database.GetDbConnection().ConnectionString))
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
                    splitOn: "Email,ImageName"))
                    .FirstOrDefault();
            }
        }

        public async Task<ProfileSettingsReadModel> GetProfileSettingsAsync(Guid id)
        {
            string sql = @"SELECT Profiles.Id, 
                         Profiles.PrivacyType AS PrivacyType
                         FROM Profiles
                         WHERE Profiles.Id = @id";

            using (DbConnection connection = new SqlConnection(_dbContext.Database.GetDbConnection().ConnectionString))
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

        public async Task<IEnumerable<ProfileFriendReadModel>> GetProfileFriendsAsync(Guid id, string nameFilter = null)
        {
            string sql = $@"SELECT Profiles.Id AS ProfileId, Profiles.Name, 
                            Profiles.AvatarImageName AS ImageName, Profiles.AvatarImageUrlPath AS UrlPath
                            FROM Friends
                            LEFT JOIN Profiles ON Profiles.Id = Friends.FriendId
                            WHERE Friends.UserId = @id AND Profiles.Name LIKE '%{@nameFilter ?? string.Empty}%'";

            using (DbConnection connection = new SqlConnection(_dbContext.Database.GetDbConnection().ConnectionString))
            {
                return (await connection
                    .QueryAsync<ProfileFriendReadModel, Image, ProfileFriendReadModel>(sql,
                    (profile, avatarImage) =>
                    {
                        profile.AvatarImage = avatarImage;
                        return profile;
                    },
                    new { id },
                    splitOn: "ImageName"));
            }
        }
    }
}
