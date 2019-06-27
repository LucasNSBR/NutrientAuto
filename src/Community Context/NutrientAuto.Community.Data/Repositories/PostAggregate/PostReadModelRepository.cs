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
using System.Data.SqlClient;
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

        public async Task<IEnumerable<PostListReadModel>> GetPostListAsync(Guid profileId, string titleFilter = null, int pageNumber = 1, int pageSize = 20)
        {
            string sql = $@"SELECT Posts.Id, Posts.ProfileId, Posts.Title, Posts.Body, Posts.DateCreated, 
                         (SELECT COUNT(PostLikes.Id) FROM PostLikes WHERE PostLikes.PostId = Posts.Id) AS LikesCount, 
                         (SELECT COUNT(Comments.Id) FROM Comments WHERE Comments.PostId = Posts.Id) AS CommentsCount,
                         Posts.PostImageName AS ImageName, Posts.PostImageUrlPath AS UrlPath 
                         FROM Posts
                         WHERE Posts.Title LIKE '%{titleFilter ?? string.Empty}%' AND Posts.ProfileId = @profileId
                         ORDER BY Posts.DateCreated DESC
                         OFFSET (@pageNumber - 1) * @pageSize ROWS
                         FETCH NEXT @pageSize ROWS ONLY";

            using (DbConnection connection = new SqlConnection(_dbContext.Database.GetDbConnection().ConnectionString))
            {
                return await connection
                    .QueryAsync<PostListReadModel, Image, PostListReadModel>(sql,
                    (post, postImage) =>
                    {
                        post.AttachedImage = postImage;
                        return post;
                    },
                    new { profileId, titleFilter = titleFilter ?? string.Empty, pageNumber, pageSize },
                    splitOn: "ImageName");
            }
        }

        public async Task<PostSummaryReadModel> GetPostSummaryAsync(Guid id)
        {
            string sql = @"SELECT Posts.Id, Posts.ProfileId, Posts.Title, Posts.Body, Posts.DateCreated, 
                         Posts.PostImageName as ImageName, Posts.PostImageUrlPath as UrlPath, 
                         Posts.HasEntityReference AS HasReference, Posts.EntityReferenceId AS ReferenceId, Posts.EntityReferenceType AS ReferenceType,
                         comments.Id AS Id, comments.PostId AS PostId, comments.ProfileId AS ProfileId, comments.Body AS Body, comments.DateCreated AS DateCreated, 
                         commenter.Name AS ProfileName,
                         replies.Id AS Id, replies.PostId AS PostId, replies.ProfileId AS ProfileId, replies.Body AS Body, replies.DateCreated AS DateCreated, replies.ReplyTo AS ReplyTo,
                         replier.Name as ProfileName,                         
                         PostLikes.ProfileId AS ProfileId, PostLikes.DateCreated AS DateCreated,
                         liker.Name AS ProfileName
                         FROM Posts
                         LEFT JOIN Comments comments ON (comments.PostId = Posts.Id AND comments.ReplyTo IS NULL)
                         LEFT JOIN Comments replies ON (replies.ReplyTo IS NOT NULL AND comments.ID = replies.ReplyTo)
                         LEFT JOIN PostLikes ON PostLikes.PostId = Posts.Id 
                         LEFT JOIN Profiles commenter ON commenter.Id = comments.ProfileId
                         LEFT JOIN Profiles replier ON replier.Id = replies.ProfileId
                         LEFT JOIN Profiles liker ON liker.Id = PostLikes.ProfileId
                         WHERE Posts.Id = @id
                         ORDER BY comments.DateCreated DESC, replies.DateCreated DESC";

            using (DbConnection connection = new SqlConnection(_dbContext.Database.GetDbConnection().ConnectionString))
            {
                Dictionary<Guid, PostSummaryReadModel> rows = new Dictionary<Guid, PostSummaryReadModel>();

                return (await connection
                    .QueryAsync<PostSummaryReadModel, Image, EntityReference, CommentReadModel, ReplyReadModel, PostLikeReadModel, PostSummaryReadModel>(sql,
                    (post, postImage, entityReference, comment, reply, postLike) =>
                    {
                        PostSummaryReadModel summary;

                        if (!rows.TryGetValue(id, out summary))
                        {
                            summary = post;
                            summary.Comments = new List<CommentReadModel>();
                            summary.Likes = new List<PostLikeReadModel>();
                            rows.Add(id, summary);
                        }

                        if (comment != null && !summary.Comments.Any(m => m.Id == comment.Id))
                        {
                            summary.Comments.Add(comment);
                            comment.Replies = new List<ReplyReadModel>();
                        }

                        if (reply != null)
                        {
                            CommentReadModel commentToReply = summary.Comments.Find(c => c.Id == reply.ReplyTo);
                            if (!commentToReply.Replies.Any(r => r.Id == reply.Id))
                                commentToReply.Replies.Add(reply);
                        }

                        if (postLike != null && !summary.Likes.Any(p => p.ProfileId == postLike.ProfileId))
                            summary.Likes.Add(postLike);

                        post.EntityReference = entityReference;
                        post.AttachedImage = postImage;
                        return post;
                    },
                    new { id },
                    splitOn: "ImageName,HasReference,Id,Id,ProfileId"))
                    .FirstOrDefault();
            }
        }
    }
}
