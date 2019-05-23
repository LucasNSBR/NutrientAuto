using Dapper;
using Microsoft.EntityFrameworkCore;
using NutrientAuto.Community.Data.Context;
using NutrientAuto.Community.Domain.Aggregates.PostAggregate;
using NutrientAuto.Community.Domain.ReadModels.PostAggregate;
using NutrientAuto.Community.Domain.Repositories.PostAggregate;
using NutrientAuto.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Data.Repositories.PostAggregate
{
    public class PostReadModelRepository : BaseReadModelRepository, IPostReadModelRepository
    {
        public PostReadModelRepository(CommunityDbContext dbContext)
            : base(dbContext)
        {
        }

        public Task<IEnumerable<PostListReadModel>> GetPostListAsync(Guid profileId, string titleFilter = null, int pageNumber = 1, int pageSize = 20)
        {
            string sql = @"SELECT Posts.Id, Posts.ProfileId, Posts.Title, Posts.Body, Posts.DateCreated, Posts.PostImageName AS PostImageName, Posts.PostImageUrlPath AS PostImageUrlPath FROM Posts
                         WHERE Title LIKE %@titleFilter%
                         ORDER BY Posts.Name
                         OFFSET (@pageNumber - 1) * @pageSize ROWS
                         FETCH NEXT @pageSize ROWS ONLY";

            using (DbConnection connection = _dbContext.Database.GetDbConnection())
            {
                return connection
                    .QueryAsync<PostListReadModel, Image, PostListReadModel>(sql,
                    (post, postImage) =>
                    {
                        post.AttachedImage = postImage;
                        return post;
                    },
                    new { titleFilter = titleFilter ?? string.Empty, pageNumber, pageSize },
                    splitOn: "PostImageName");
            }
        }

        public async Task<PostSummaryReadModel> GetPostSummaryAsync(Guid id)
        {
            string sql = @"SELECT Posts.Id, Posts.ProfileId, Posts.Title, Posts.Body, Posts.DateCreated, 
                         Posts.PostImageName AS PostImageName, Posts.PostImageUrlPath AS PostImageUrlPath
                         Posts.HasEntityReference AS HasEntityReference, Posts.EntityReferenceId AS EntityReferenceId, Posts.EntityReferenceType AS EntityReferenceType FROM Posts
                         WHERE Id = @id";

            using (DbConnection connection = _dbContext.Database.GetDbConnection())
            {
                return (await connection
                    .QueryAsync<PostSummaryReadModel, Image, EntityReference, PostSummaryReadModel>(sql,
                    (post, postImage, entityReference) =>
                    {
                        post.AttachedImage = postImage;
                        post.EntityReference = entityReference;
                        return post;
                    },
                    new { id },
                    splitOn: "PostImageName"))
                    .FirstOrDefault();
            }
        }
    }
}
