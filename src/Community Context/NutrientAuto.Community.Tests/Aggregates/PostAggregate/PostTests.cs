using Microsoft.VisualStudio.TestTools.UnitTesting;
using NutrientAuto.Community.Domain.Aggregates.CommentAggregate;
using NutrientAuto.Community.Domain.Aggregates.PostAggregate;
using NutrientAuto.Shared.ValueObjects;
using System;
using System.Linq;

namespace NutrientAuto.Community.Tests.Aggregates.PostAggregate
{
    [TestClass]
    public class PostTests
    {
        public Post GetNewSimplePost()
        {
            Post post = new Post(Guid.NewGuid(), "Post novo", "Post simples", null, null);
            return post;
        }

        #region Initialization
        [TestMethod]
        public void ShouldSetInitialPostVariables()
        {
            Post post = GetNewSimplePost();

            Assert.AreNotEqual(Guid.Empty, post.ProfileId);
            Assert.AreEqual("Post novo", post.Title);
            Assert.AreEqual("Post simples", post.Body);
            Assert.AreEqual(new Image(null, null), post.AttachedImage);
            Assert.AreEqual(EntityReference.None(), post.EntityReference);
        }

        [TestMethod]
        public void ShouldSetEntityReferencePost()
        {
            Guid goalId = Guid.NewGuid();
            EntityReference entityReference = EntityReference.Goal(goalId);
            Post post = new Post(Guid.NewGuid(), "Post novo", "Post com EntityReference!!!", entityReference, null);

            Assert.IsNotNull(post.EntityReference);
            Assert.IsTrue(post.EntityReference.HasReference);
            Assert.AreEqual(EntityReferenceType.Goal, post.EntityReference.ReferenceType);
            Assert.AreEqual(goalId, post.EntityReference.ReferenceId);
        }

        [TestMethod]
        public void ShouldSetAttachedImagePost()
        {
            Image postImage = new Image("blobs.azure.com/a1e51afe50e616", "DSC_00021");
            Post post = new Post(Guid.NewGuid(), "Post novo", "Post com Imagem!!!", null, postImage);

            Assert.AreEqual("blobs.azure.com/a1e51afe50e616", post.AttachedImage.UrlPath);
            Assert.AreEqual("DSC_00021", post.AttachedImage.ImageName);
        }
        #endregion

        #region Likes
        [TestMethod]
        public void ShouldAddPostLike()
        {
            PostLike like = new PostLike(Guid.NewGuid());

            Post post = GetNewSimplePost();
            post.AddLike(like);

            Assert.IsTrue(post.IsValid);
            Assert.AreEqual(1, post.Likes.Count);
        }

        [TestMethod]
        public void ShouldFailPostLikeAlreadyLiked()
        {
            PostLike like = new PostLike(Guid.NewGuid());

            Post post = GetNewSimplePost();

            post.AddLike(like);
            Assert.AreEqual(1, post.Likes.Count);
            post.AddLike(like);

            Assert.IsFalse(post.IsValid);
            Assert.AreEqual("Você já curtiu essa publicação uma vez.", post.GetNotifications().FirstOrDefault().Description);
        }

        [TestMethod]
        public void ShouldRemovePostLike()
        {
            PostLike like = new PostLike(Guid.NewGuid());

            Post post = GetNewSimplePost();

            post.AddLike(like);
            Assert.AreEqual(1, post.Likes.Count);
            post.RemoveLike(like);

            Assert.IsTrue(post.IsValid);
            Assert.AreEqual(0, post.Likes.Count);
        }

        [TestMethod]
        public void ShouldFailToRemovePostLikeNotLikedYet()
        {
            PostLike like = new PostLike(Guid.NewGuid());

            Post post = GetNewSimplePost();
            post.RemoveLike(like);

            Assert.IsFalse(post.IsValid);
            Assert.AreEqual("Você ainda não curtiu essa publicação.", post.GetNotifications().FirstOrDefault().Description);
        }
        #endregion

        #region Comments
        [TestMethod]
        public void ShouldAddComment()
        {
            Post post = GetNewSimplePost();
            Comment comment = new Comment(post.Id, post.ProfileId, "OLÁ, MUNDO!");

            post.AddComment(comment);

            Assert.IsTrue(post.IsValid);
            Assert.AreEqual(1, post.Comments.Count);
        }

        [TestMethod]
        public void ShouldRemoveComment()
        {
            Post post = GetNewSimplePost();
            Comment comment = new Comment(post.Id, post.ProfileId, "OLÁ, MUNDO!");

            post.AddComment(comment);
            Assert.AreEqual(1, post.Comments.Count);
            post.RemoveComment(comment);

            Assert.IsTrue(post.IsValid);
            Assert.AreEqual(0, post.Comments.Count);
        }

        [TestMethod]
        public void ShouldFailToRemoveCommentDoesntExists()
        {
            Post post = GetNewSimplePost();
            Comment comment = new Comment(post.Id, post.ProfileId, "OLÁ, MUNDO!");
            
            post.RemoveComment(comment);

            Assert.IsFalse(post.IsValid);
            Assert.AreEqual("Esse comentário não foi encontrado nessa publicação.", post.GetNotifications().FirstOrDefault().Description);
        }
        #endregion
    }
}
