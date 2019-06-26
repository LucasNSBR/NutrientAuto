using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NutrientAuto.Community.Domain.Aggregates.GoalAggregate;
using NutrientAuto.Community.Domain.CommandHandlers.GoalAggregate;
using NutrientAuto.Community.Domain.Commands.GoalAggregate;
using NutrientAuto.Community.Domain.Context;
using NutrientAuto.Community.Tests.Fakes.GoalAggregate;
using NutrientAuto.CrossCutting.HttpService.HttpContext;
using NutrientAuto.CrossCutting.UnitOfwork.Abstractions;
using NutrientAuto.CrossCutting.UnitOfWork;
using NutrientAuto.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Tests.CommandHandlers.GoalAggregate
{
    [TestClass]
    public class GoalCommandHandlerTests
    {
        #region Arrange
        public IMediator GetMediator()
        {
            Mock<IMediator> mock = new Mock<IMediator>();
            mock.Setup(mediatr => mediatr.Publish(It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            return mock.Object;
        }

        public IIdentityService GetIdentityService(Guid? id = null)
        {
            Mock<IIdentityService> mock = new Mock<IIdentityService>();
            mock.Setup(identity => identity.GetUserId())
                .Returns(id ?? Guid.NewGuid());

            return mock.Object;
        }

        public IUnitOfWork<ICommunityDbContext> GetUnitOfWork()
        {
            Mock<IUnitOfWork<ICommunityDbContext>> mock = new Mock<IUnitOfWork<ICommunityDbContext>>();
            mock.Setup(uow => uow.CommitAsync())
                .ReturnsAsync(CommitResult.Ok());

            return mock.Object;
        }

        public ILogger<GoalCommandHandler> GetLogger()
        {
            Mock<ILogger<GoalCommandHandler>> mock = new Mock<ILogger<GoalCommandHandler>>();
            mock.Setup(logger => logger.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<object[]>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()));

            return mock.Object;
        }
        #endregion

        #region Register
        [TestMethod]
        public async Task ShouldRegisterGoal()
        {
            RegisterGoalCommand command = new RegisterGoalCommand
            {
                Title = "Testes de Goal",
                Details = "Registrando um goal para testes unitários",
                WritePost = false
            };

            GoalFakeRepository goalFakeRepository = new GoalFakeRepository();
            GoalCommandHandler handler = new GoalCommandHandler(
                goalFakeRepository,
                GetIdentityService(null),
                GetMediator(),
                GetUnitOfWork(),
                GetLogger()
                );

            CommandResult commandResult = await handler.Handle(command, default(CancellationToken));

            Assert.IsTrue(commandResult.Success);
            Assert.AreEqual(1, goalFakeRepository._goals.Count);
        }
        #endregion

        #region Update
        [TestMethod]
        public async Task ShouldUpdateGoal()
        {
            Guid profileId = Guid.NewGuid();

            List<Goal> goals = new List<Goal>
            {
                new Goal(profileId, "Novo para teste", "Testando")
            };

            UpdateGoalCommand command = new UpdateGoalCommand
            {
                GoalId = goals.First().Id,
                Title = "Testes de Goal",
                Details = "Registrando um goal para testes unitários",
                WritePost = false
            };

            GoalFakeRepository goalFakeRepository = new GoalFakeRepository(goals);
            GoalCommandHandler handler = new GoalCommandHandler(
                goalFakeRepository,
                GetIdentityService(profileId),
                GetMediator(),
                GetUnitOfWork(),
                GetLogger()
                );

            Assert.AreEqual("Novo para teste", goals.First().Title);

            CommandResult commandResult = await handler.Handle(command, default(CancellationToken));

            Assert.AreEqual("Testes de Goal", goals.First().Title);
            Assert.IsTrue(commandResult.Success);
        }

        [TestMethod]
        public async Task ShouldFailToUpdateGoalIdNotFound()
        {
            UpdateGoalCommand command = new UpdateGoalCommand
            {
                GoalId = Guid.NewGuid(),
                Title = "Testes de Goal",
                Details = "Registrando um goal para testes unitários",
                WritePost = false
            };

            GoalFakeRepository goalFakeRepository = new GoalFakeRepository();
            GoalCommandHandler handler = new GoalCommandHandler(
                goalFakeRepository,
                GetIdentityService(),
                GetMediator(),
                GetUnitOfWork(),
                GetLogger()
                );

            CommandResult commandResult = await handler.Handle(command, default(CancellationToken));

            Assert.IsFalse(commandResult.Success);
            Assert.AreEqual("Falha ao buscar Objetivo no banco de dados.", commandResult.Notifications.FirstOrDefault().Description);
        }

        [TestMethod]
        public async Task ShouldFailToUpdateGoalUserNotOwner()
        {
            Guid profileId = Guid.NewGuid();

            List<Goal> goals = new List<Goal>
            {
                new Goal(profileId, "Novo para teste", "Testando")
            };

            UpdateGoalCommand command = new UpdateGoalCommand
            {
                GoalId = goals.First().Id,
                Title = "Testes de Goal",
                Details = "Registrando um goal para testes unitários",
                WritePost = false
            };

            GoalFakeRepository goalFakeRepository = new GoalFakeRepository(goals);
            GoalCommandHandler handler = new GoalCommandHandler(
                goalFakeRepository,
                GetIdentityService(null),
                GetMediator(),
                GetUnitOfWork(),
                GetLogger()
                );

            CommandResult commandResult = await handler.Handle(command, default(CancellationToken));

            Assert.IsFalse(commandResult.Success);
            Assert.AreEqual("Falha ao buscar Objetivo no banco de dados.", commandResult.Notifications.FirstOrDefault().Description);
        }
        #endregion

        #region Remove 
        [TestMethod]
        public async Task ShouldRemoveGoal()
        {
            Guid profileId = Guid.NewGuid();

            List<Goal> goals = new List<Goal>
            {
                new Goal(profileId, "Novo para teste", "Testando")
            };

            RemoveGoalCommand command = new RemoveGoalCommand
            {
                GoalId = goals.First().Id
            };

            GoalFakeRepository goalFakeRepository = new GoalFakeRepository(goals);
            GoalCommandHandler handler = new GoalCommandHandler(
                goalFakeRepository,
                GetIdentityService(profileId),
                GetMediator(),
                GetUnitOfWork(),
                GetLogger()
                );

            Assert.AreEqual(1, goalFakeRepository._goals.Count);

            CommandResult commandResult = await handler.Handle(command, default(CancellationToken));

            Assert.IsTrue(commandResult.Success);
            Assert.AreEqual(0, goalFakeRepository._goals.Count);
        }

        [TestMethod]
        public async Task ShouldFailToRemoveGoalIdNotFound()
        {
            RemoveGoalCommand command = new RemoveGoalCommand
            {
                GoalId = Guid.NewGuid()
            };

            GoalFakeRepository goalFakeRepository = new GoalFakeRepository();
            GoalCommandHandler handler = new GoalCommandHandler(
                goalFakeRepository,
                GetIdentityService(),
                GetMediator(),
                GetUnitOfWork(),
                GetLogger()
                );

            CommandResult commandResult = await handler.Handle(command, default(CancellationToken));

            Assert.IsFalse(commandResult.Success);
            Assert.AreEqual("Falha ao buscar Objetivo no banco de dados.", commandResult.Notifications.FirstOrDefault().Description);
        }

        [TestMethod]
        public async Task ShouldFailToRemoveGoalUserNotOwner()
        {
            List<Goal> goals = new List<Goal>
            {
                new Goal(Guid.NewGuid(), "Novo para teste", "Testando")
            };

            RemoveGoalCommand command = new RemoveGoalCommand
            {
                GoalId = goals.First().Id
            };

            GoalFakeRepository goalFakeRepository = new GoalFakeRepository(goals);
            GoalCommandHandler handler = new GoalCommandHandler(
                goalFakeRepository,
                GetIdentityService(),
                GetMediator(),
                GetUnitOfWork(),
                GetLogger()
                );

            CommandResult commandResult = await handler.Handle(command, default(CancellationToken));

            Assert.IsFalse(commandResult.Success);
            Assert.AreEqual("Falha ao buscar Objetivo no banco de dados.", commandResult.Notifications.FirstOrDefault().Description);
        }
        #endregion

        #region Set Completed
        [TestMethod]
        public async Task ShouldSetCompleted()
        {
            Guid profileId = Guid.NewGuid();

            List<Goal> goals = new List<Goal>
            {
                new Goal(profileId, "Novo para teste", "Testando")
            };

            SetCompletedGoalCommand command = new SetCompletedGoalCommand
            {
                GoalId = goals.FirstOrDefault().Id,
                AccomplishmentDetails = "Ëu completei !!!!",
                DateCompleted = DateTime.Now
            };

            GoalFakeRepository goalFakeRepository = new GoalFakeRepository(goals);
            GoalCommandHandler handler = new GoalCommandHandler(
                goalFakeRepository,
                GetIdentityService(profileId),
                GetMediator(),
                GetUnitOfWork(),
                GetLogger()
                );

            CommandResult commandResult = await handler.Handle(command, default(CancellationToken));

            Assert.IsTrue(commandResult.Success);
            Assert.IsTrue(goals.First().Status.IsCompleted);
        }

        [TestMethod]
        public async Task ShouldFailToSetCompletedTwice()
        {
            Guid profileId = Guid.NewGuid();

            List<Goal> goals = new List<Goal>
            {
                new Goal(profileId, "Novo para teste", "Testando")
            };

            SetCompletedGoalCommand command = new SetCompletedGoalCommand
            {
                GoalId = goals.FirstOrDefault().Id,
                AccomplishmentDetails = "Foi completado primeira vez, será completado de novo",
                DateCompleted = DateTime.Now
            };

            GoalFakeRepository goalFakeRepository = new GoalFakeRepository(goals);
            GoalCommandHandler handler = new GoalCommandHandler(
                goalFakeRepository,
                GetIdentityService(profileId),
                GetMediator(),
                GetUnitOfWork(),
                GetLogger()
                );

            CommandResult commandResult = await handler.Handle(command, default(CancellationToken));
            Assert.IsTrue(commandResult.Success);
            Assert.IsTrue(goals.First().Status.IsCompleted);

            CommandResult commandResultTwo = await handler.Handle(command, default(CancellationToken));
            Assert.IsFalse(commandResultTwo.Success);
            Assert.AreEqual("Esse objetivo já está marcado como completo.", commandResultTwo.Notifications.FirstOrDefault().Description);
        }

        [TestMethod]
        public async Task ShouldFailToSetGoalCompletedUserNotOwner()
        {
            List<Goal> goals = new List<Goal>
            {
                new Goal(Guid.NewGuid(), "Novo para teste", "Testando")
            };
            
            SetCompletedGoalCommand command = new SetCompletedGoalCommand
            {
                GoalId = goals.FirstOrDefault().Id
            };

            GoalFakeRepository goalFakeRepository = new GoalFakeRepository(goals);
            GoalCommandHandler handler = new GoalCommandHandler(
                goalFakeRepository,
                GetIdentityService(),
                GetMediator(),
                GetUnitOfWork(),
                GetLogger()
                );

            CommandResult commandResult = await handler.Handle(command, default(CancellationToken));

            Assert.IsFalse(commandResult.Success);
            Assert.AreEqual("Falha ao buscar Objetivo no banco de dados.", commandResult.Notifications.FirstOrDefault().Description);
        }

        [TestMethod]
        public async Task ShouldFailToSetGoalCompletedIdNotFound()
        {
            Guid profileId = Guid.NewGuid();

            List<Goal> goals = new List<Goal>
            {
                new Goal(profileId, "Novo para teste", "Testando")
            };

            goals.First().SetCompleted(DateTime.Now, "Completado para testes");

            SetCompletedGoalCommand command = new SetCompletedGoalCommand
            {
                GoalId = Guid.NewGuid()
            };

            GoalFakeRepository goalFakeRepository = new GoalFakeRepository(goals);
            GoalCommandHandler handler = new GoalCommandHandler(
                goalFakeRepository,
                GetIdentityService(profileId),
                GetMediator(),
                GetUnitOfWork(),
                GetLogger()
                );

            CommandResult commandResult = await handler.Handle(command, default(CancellationToken));

            Assert.IsFalse(commandResult.Success);
            Assert.AreEqual("Falha ao buscar Objetivo no banco de dados.", commandResult.Notifications.FirstOrDefault().Description);
        }
        #endregion

        #region Set Uncompleted
        [TestMethod]
        public async Task ShouldSetUncompleted()
        {
            Guid profileId = Guid.NewGuid();

            List<Goal> goals = new List<Goal>
            {
                new Goal(profileId, "Novo para teste", "Testando")
            };

            goals.First().SetCompleted(DateTime.Now, "Completado para testes");

            SetUncompletedGoalCommand command = new SetUncompletedGoalCommand
            {
                GoalId = goals.FirstOrDefault().Id
            };

            GoalFakeRepository goalFakeRepository = new GoalFakeRepository(goals);
            GoalCommandHandler handler = new GoalCommandHandler(
                goalFakeRepository,
                GetIdentityService(profileId),
                GetMediator(),
                GetUnitOfWork(),
                GetLogger()
                );

            CommandResult commandResult = await handler.Handle(command, default(CancellationToken));

            Assert.IsTrue(commandResult.Success);
            Assert.IsFalse(goals.First().Status.IsCompleted);
        }

        [TestMethod]
        public async Task ShouldFailToSetUncompletedTwice()
        {
            Guid profileId = Guid.NewGuid();

            List<Goal> goals = new List<Goal>
            {
                new Goal(profileId, "Novo para teste", "Testando")
            };

            goals.First().SetCompleted(DateTime.Now, "Completado para testes");

            SetUncompletedGoalCommand command = new SetUncompletedGoalCommand
            {
                GoalId = goals.FirstOrDefault().Id
            };

            GoalFakeRepository goalFakeRepository = new GoalFakeRepository(goals);
            GoalCommandHandler handler = new GoalCommandHandler(
                goalFakeRepository,
                GetIdentityService(profileId),
                GetMediator(),
                GetUnitOfWork(),
                GetLogger()
                );

            CommandResult commandResult = await handler.Handle(command, default(CancellationToken));

            Assert.IsTrue(commandResult.Success);
            Assert.IsFalse(goals.First().Status.IsCompleted);

            CommandResult commandResultTwo = await handler.Handle(command, default(CancellationToken));
            Assert.IsFalse(commandResultTwo.Success);
            Assert.AreEqual("Esse objetivo já está marcado como incompleto.", commandResultTwo.Notifications.FirstOrDefault().Description);
        }

        [TestMethod]
        public async Task ShouldFailToSetGoalUncompletedUserNotOwner()
        {
            List<Goal> goals = new List<Goal>
            {
                new Goal(Guid.NewGuid(), "Novo para teste", "Testando")
            };

            goals.First().SetCompleted(DateTime.Now, "Completado para testes");

            SetUncompletedGoalCommand command = new SetUncompletedGoalCommand
            {
                GoalId = goals.FirstOrDefault().Id
            };

            GoalFakeRepository goalFakeRepository = new GoalFakeRepository(goals);
            GoalCommandHandler handler = new GoalCommandHandler(
                goalFakeRepository,
                GetIdentityService(),
                GetMediator(),
                GetUnitOfWork(),
                GetLogger()
                );

            CommandResult commandResult = await handler.Handle(command, default(CancellationToken));

            Assert.IsFalse(commandResult.Success);
            Assert.AreEqual("Falha ao buscar Objetivo no banco de dados.", commandResult.Notifications.FirstOrDefault().Description);
        }

        [TestMethod]
        public async Task ShouldFailToSetGoalUncompletedIdNotFound()
        {
            Guid profileId = Guid.NewGuid();

            List<Goal> goals = new List<Goal>
            {
                new Goal(profileId, "Novo para teste", "Testando")
            };

            goals.First().SetCompleted(DateTime.Now, "Completado para testes");

            SetUncompletedGoalCommand command = new SetUncompletedGoalCommand
            {
                GoalId = Guid.NewGuid()
            };

            GoalFakeRepository goalFakeRepository = new GoalFakeRepository(goals);
            GoalCommandHandler handler = new GoalCommandHandler(
                goalFakeRepository,
                GetIdentityService(profileId),
                GetMediator(),
                GetUnitOfWork(),
                GetLogger()
                );

            CommandResult commandResult = await handler.Handle(command, default(CancellationToken));

            Assert.IsFalse(commandResult.Success);
            Assert.AreEqual("Falha ao buscar Objetivo no banco de dados.", commandResult.Notifications.FirstOrDefault().Description);
        }
        #endregion
    }
}
