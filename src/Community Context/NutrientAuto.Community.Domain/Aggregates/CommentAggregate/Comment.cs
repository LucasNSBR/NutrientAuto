using NutrientAuto.Community.Domain.Aggregates.SeedWork;
using NutrientAuto.Shared.Entities;
using System;
using System.Collections.Generic;

namespace NutrientAuto.Community.Domain.Aggregates.CommentAggregate
{
    public class Comment : Entity<Comment>, IAggregateRoot, IProfileEntity
    {
        public Guid PostId { get; private set; }
        public Guid ProfileId { get; private set; }
        public string Body { get; private set; }
        public DateTime DateCreated { get; private set; }

        private readonly List<Comment> _replies;
        public IReadOnlyList<Comment> Replies => _replies;

        public Guid? ReplyTo { get; private set; }

        protected Comment()
        {
        }

        public Comment(Guid postId, Guid profileId, string body)
        {
            PostId = postId;
            ProfileId = profileId;
            Body = body;
            DateCreated = DateTime.Now;

            _replies = new List<Comment>();
        }

        private Comment(Guid postId, Guid profileId, string body, Guid replyTo)
        {
            PostId = postId;
            ProfileId = profileId;
            Body = body;
            _replies = new List<Comment>();

            ReplyTo = replyTo;
        }

        public Comment Reply(Guid profileId, string body)
        {
            Comment replyComment = new Comment(PostId, profileId, body, Id);
            _replies.Add(replyComment);

            return replyComment;
        }
    }
}
