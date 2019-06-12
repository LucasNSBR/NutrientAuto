using Dapper;
using Microsoft.EntityFrameworkCore;
using NutrientAuto.Community.Data.Context;
using NutrientAuto.Community.Domain.ReadModels.FriendshipRequestAggregate;
using NutrientAuto.Community.Domain.Repositories.FriendshipRequestAggregate;
using NutrientAuto.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Data.Repositories.FriendshipRequestAggregate
{
    public class FriendshipRequestReadModelRepository : BaseReadModelRepository, IFriendshipRequestReadModelRepository
    {
        public FriendshipRequestReadModelRepository(CommunityDbContext dbContext)
            : base(dbContext)
        {
        }
        
        public async Task<IEnumerable<FriendshipRequestListReadModel>> GetFriendshipRequestList(Guid requestedId, string nameFilter = null, int pageNumber = 1, int pageSize = 20)
        {
            string sql = $@"SELECT FriendshipRequests.Id, FriendshipRequests.RequesterId, FriendshipRequests.DateCreated, 
                         Profiles.Name AS RequesterName, Profiles.AvatarImageName AS ImageName, Profiles.AvatarImageUrlPath AS UrlPath 
                         FROM FriendshipRequests
                         JOIN Profiles ON FriendshipRequests.RequesterId = Profiles.Id 
                         WHERE Profiles.Name LIKE '%{@nameFilter ?? string.Empty}%' AND FriendshipRequests.RequestedId = @requestedId AND FriendshipRequests.Status = 0
                         ORDER BY FriendshipRequests.DateCreated DESC
                         OFFSET (@pageNumber - 1) * @pageSize ROWS
                         FETCH NEXT @pageSize ROWS ONLY";

            using (DbConnection connection = new SqlConnection(_dbContext.Database.GetDbConnection().ConnectionString))
            {
                return await connection
                    .QueryAsync<FriendshipRequestListReadModel, Image, FriendshipRequestListReadModel>(sql,
                    (friendshipRequest, avatarImage) =>
                    {
                        friendshipRequest.RequesterAvatarImage = avatarImage;
                        return friendshipRequest;
                    },
                    new { requestedId, nameFilter = nameFilter ?? string.Empty, pageNumber, pageSize },
                    splitOn: "ImageName");
            }
        }

        public async Task<IEnumerable<FriendshipRequestSentListReadModel>> GetFriendshipRequestSentList(Guid requesterId, string nameFilter = null, int pageNumber = 1, int pageSize = 20)
        {
            string sql = $@"SELECT FriendshipRequests.Id, FriendshipRequests.RequestedId, FriendshipRequests.DateCreated, 
                         Profiles.Name AS RequestedName, Profiles.AvatarImageName AS ImageName, Profiles.AvatarImageUrlPath AS UrlPath 
                         FROM FriendshipRequests
                         JOIN Profiles ON FriendshipRequests.RequestedId = Profiles.Id 
                         WHERE Profiles.Name LIKE '%{@nameFilter ?? string.Empty}%' AND FriendshipRequests.RequesterId = @requesterId AND FriendshipRequests.Status = 0
                         ORDER BY FriendshipRequests.DateCreated DESC
                         OFFSET (@pageNumber - 1) * @pageSize ROWS
                         FETCH NEXT @pageSize ROWS ONLY";

            using (DbConnection connection = new SqlConnection(_dbContext.Database.GetDbConnection().ConnectionString))
            {
                return await connection
                    .QueryAsync<FriendshipRequestSentListReadModel, Image, FriendshipRequestSentListReadModel>(sql,
                    (friendshipRequestSent, avatarImage) =>
                    {
                        friendshipRequestSent.RequestedAvatarImage = avatarImage;
                        return friendshipRequestSent;
                    },
                    new { requesterId, nameFilter = nameFilter ?? string.Empty, pageNumber, pageSize },
                    splitOn: "ImageName");
            }
        }
    }
}
