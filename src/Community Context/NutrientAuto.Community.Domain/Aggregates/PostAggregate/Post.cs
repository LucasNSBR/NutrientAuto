using NutrientAuto.Community.Domain.Aggregates.CommentAggregate;
using NutrientAuto.Community.Domain.Aggregates.ProfileAggregate;
using NutrientAuto.Community.Domain.Aggregates.SeedWork;
using NutrientAuto.Shared.Entities;
using NutrientAuto.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NutrientAuto.Community.Domain.Aggregates.PostAggregate
{
    public class Post : Entity<Post>, IAggregateRoot, IProfileEntity
    {
        public Guid ProfileId { get; private set; }
        public string Title { get; private set; }
        public string Body { get; private set; }
        public Image AttachedImage { get; private set; }
        public EntityReference EntityReference { get; protected set; }
        public DateTime DateCreated { get; private set; }

        private readonly List<PostLike> _likes = new List<PostLike>();
        public IReadOnlyList<PostLike> Likes => _likes;

        private readonly List<Comment> _comments = new List<Comment>();
        public IReadOnlyList<Comment> Comments => _comments;
        
        protected Post()
        {
        }

        public Post(Guid profileId, string title, string body, EntityReference entityReference = null, Image attachedImage = null)
        {
            ProfileId = profileId;
            Title = title;
            Body = body;
            AttachedImage = attachedImage ?? Image.Default();
            EntityReference = entityReference ?? EntityReference.None();
            DateCreated = DateTime.Now;
        }

        public PostLike FindLikeByProfileId(Guid profileId) => _likes.Find(l => l.ProfileId == profileId);
        public PostLike FindLikeByProfile(Profile ProfileLiker) => _likes.Find(l => l.ProfileId == ProfileLiker.Id);

        public void AddLike(PostLike like)
        {
            PostLike existingLike = FindLikeByProfileId(like.ProfileId);

            if (existingLike != null)
                AddNotification("Erro ao curtir", "Você já curtiu essa publicação uma vez.");

            _likes.Add(like);
        }

        public void RemoveLike(PostLike like)
        {
            if (!_likes.Contains(like))
                AddNotification("Erro ao descurtir", "Você ainda não curtiu essa publicação.");

            _likes.Remove(like);
        }

        public Comment FindCommentById(Guid commentId) => _comments.Find(c => c.Id == commentId);
        public IReadOnlyList<Comment> FindCommentsByProfile(Profile profile) => _comments.Where(c => c.ProfileId == profile.Id).ToList();

        public void AddComment(Comment comment)
        {
            _comments.Add(comment);
        }

        public void RemoveComment(Comment comment)
        {
            if (!_comments.Contains(comment))
                AddNotification("Comentário indisponível", "Esse comentário não foi encontrado nessa publicação.");

            _comments.Remove(comment);
        }
    }
}
