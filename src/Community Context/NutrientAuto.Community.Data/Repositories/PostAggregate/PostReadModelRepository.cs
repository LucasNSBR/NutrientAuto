using Dapper;
using Microsoft.EntityFrameworkCore;
using NutrientAuto.Community.Data.Context;
using NutrientAuto.Community.Domain.Aggregates.CommentAggregate;
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
            string sql = @"SELECT Posts.Id, Posts.ProfileId, Posts.Title, Posts.Body, Posts.DateCreated, Posts.PostImageName, Posts.PostImageUrlPath, (SELECT COUNT(PostLike.Id) FROM PostLike WHERE PostLike.PostId = Posts.Id) AS LikesCount, (SELECT COUNT(Comments.Id) FROM Comments WHERE Comments.PostId = Posts.Id) AS CommentsCount
                         FROM Posts
                         WHERE Posts.Title LIKE '%@titleFilter%'
                         ORDER BY Posts.DateCreated DESC
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
            string sql = @"SELECT Posts.Id, Posts.ProfileId, Posts.Title, Posts.Body, Posts.DateCreated, Posts.PostImageName, Posts.PostImageUrlPath, Posts.HasEntityReference, Posts.EntityReferenceId, Posts.EntityReferenceType, PostLikes.ProfileId AS LikerId, PostLikes.DateCreated, Comments.ProfileId as CommenterId, Comments.Body, Comments.DateCreated
                         FROM Posts
                         LEFT JOIN Comments ON Posts.Id = Comments.PostId 
                         LEFT JOIN PostLikes ON Posts.Id = PostLike.PostId
                         WHERE Id = @id";

            using (DbConnection connection = _dbContext.Database.GetDbConnection())
            {
                return (await connection
                    .QueryAsync<PostSummaryReadModel, Image, EntityReference, IEnumerable<PostLike>, IEnumerable<Comment>, PostSummaryReadModel>(sql,
                    (post, postImage, entityReference, postLikes, comments) =>
                    {
                        post.AttachedImage = postImage;
                        post.EntityReference = entityReference;
                        post.Likes = postLikes.ToList();
                        post.Comments = comments.ToList();
                        return post;
                    },
                    new { id },
                    splitOn: "PostImageName,HasEntityReference,LikerId,CommenterId"))
                    .FirstOrDefault();
            }
        }
    }
}
