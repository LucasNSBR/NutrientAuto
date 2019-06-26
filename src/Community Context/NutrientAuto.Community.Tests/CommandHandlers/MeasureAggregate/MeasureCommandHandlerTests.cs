using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NutrientAuto.Community.Domain.CommandHandlers.MeasureAggregate;
using NutrientAuto.Community.Domain.Commands.MeasureAggregate;
using NutrientAuto.Community.Domain.Context;
using NutrientAuto.CrossCutting.HttpService.HttpContext;
using NutrientAuto.CrossCutting.Mapping.Profiles.Community;
using NutrientAuto.CrossCutting.UnitOfwork.Abstractions;
using NutrientAuto.CrossCutting.UnitOfWork;
using NutrientAuto.Shared.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Tests.CommandHandlers.MeasureAggregate
{
    [TestClass]
    public class MeasureCommandHandlerTests
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

        public IMapper GetMapper()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg => new CommunityMappingProfile());

            return Mapper.Instance;
        }

        public ILogger<MeasureCommandHandler> GetLogger()
        {
            Mock<ILogger<MeasureCommandHandler>> mock = new Mock<ILogger<MeasureCommandHandler>>();
            mock.Setup(logger => logger.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<object[]>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()));

            return mock.Object;
        }
        #endregion

        //#region Register
        //[TestMethod]
        //public async Task ShouldRegisterReminder()
        //{
        //    RegisterMeasureCommand command = new RegisterMeasureCommand
        //    {
        //    };

        //    ReminderFakeRepository reminderFakeRepository = new ReminderFakeRepository();
        //    MeasureCommandHandler handler = new MeasureCommandHandler(
        //        reminderFakeRepository,
        //        GetMapper(),
        //        GetIdentityService(null),
        //        GetMediator(),
        //        GetUnitOfWork(),
        //        GetLogger()
        //        );

        //    CommandResult commandResult = await handler.Handle(command, default(CancellationToken));

        //    Assert.IsTrue(commandResult.Success);
        //    Assert.AreEqual(1, reminderFakeRepository._reminders.Count);
        //}
        //#endregion

        //#region Update
        //[TestMethod]
        //public async Task ShouldUpdateReminder()
        //{
        //    Guid profileId = Guid.NewGuid();

        //    List<Reminder> reminders = new List<Reminder>
        //    {
        //        new Reminder(profileId, true, "Novo para teste", "Testando", new Time(9, 0, 0))
        //    };

        //    UpdateReminderCommand command = new UpdateReminderCommand
        //    {
        //        ReminderId = reminders.First().Id,
        //        Title = "Testes de Reminder",
        //        Details = "Updatando um Reminder para testes unitários",
        //    };

        //    ReminderFakeRepository reminderFakeRepository = new ReminderFakeRepository(reminders);
        //    ReminderCommandHandler handler = new ReminderCommandHandler(
        //        reminderFakeRepository,
        //        GetMapper(),
        //        GetIdentityService(profileId),
        //        GetMediator(),
        //        GetUnitOfWork(),
        //        GetLogger()
        //        );

        //    Assert.AreEqual("Novo para teste", reminders.First().Title);

        //    CommandResult commandResult = await handler.Handle(command, default(CancellationToken));

        //    Assert.AreEqual("Testes de Reminder", reminders.First().Title);
        //    Assert.IsTrue(commandResult.Success);
        //}

        //[TestMethod]
        //public async Task ShouldFailToUpdateReminderIdNotFound()
        //{
        //    UpdateReminderCommand command = new UpdateReminderCommand
        //    {
        //        ReminderId = Guid.NewGuid(),
        //        Title = "Testes de Reminder",
        //        Details = "Updatando um Reminder para testes unitários",
        //    };

        //    ReminderFakeRepository reminderFakeRepository = new ReminderFakeRepository();
        //    ReminderCommandHandler handler = new ReminderCommandHandler(
        //        reminderFakeRepository,
        //        GetMapper(),
        //        GetIdentityService(),
        //        GetMediator(),
        //        GetUnitOfWork(),
        //        GetLogger()
        //        );

        //    CommandResult commandResult = await handler.Handle(command, default(CancellationToken));

        //    Assert.IsFalse(commandResult.Success);
        //    Assert.AreEqual("Falha ao buscar Lembrete no banco de dados.", commandResult.Notifications.FirstOrDefault().Description);
        //}

        //[TestMethod]
        //public async Task ShouldFailToUpdateReminderUserNotOwner()
        //{
        //    Guid profileId = Guid.NewGuid();

        //    List<Reminder> reminders = new List<Reminder>
        //    {
        //        new Reminder(profileId, true, "Novo para teste", "Testando", new Time(9, 0, 0))
        //    };

        //    UpdateReminderCommand command = new UpdateReminderCommand
        //    {
        //        ReminderId = reminders.First().Id,
        //        Title = "Testes de Reminder",
        //        Details = "Registrando um Reminder para testes unitários",
        //    };

        //    ReminderFakeRepository reminderFakeReposiotry = new ReminderFakeRepository(reminders);
        //    ReminderCommandHandler handler = new ReminderCommandHandler(
        //        reminderFakeReposiotry,
        //        GetMapper(),
        //        GetIdentityService(null),
        //        GetMediator(),
        //        GetUnitOfWork(),
        //        GetLogger()
        //        );

        //    CommandResult commandResult = await handler.Handle(command, default(CancellationToken));

        //    Assert.IsFalse(commandResult.Success);
        //    Assert.AreEqual("Falha ao buscar Lembrete no banco de dados.", commandResult.Notifications.FirstOrDefault().Description);
        //}
        //#endregion

        //#region Remove 
        //[TestMethod]
        //public async Task ShouldRemoveReminder()
        //{
        //    Guid profileId = Guid.NewGuid();

        //    List<Reminder> reminders = new List<Reminder>
        //    {
        //        new Reminder(profileId, true, "Novo para teste", "Testando", new Time(9, 0, 0))
        //    };

        //    RemoveReminderCommand command = new RemoveReminderCommand
        //    {
        //        ReminderId = reminders.First().Id
        //    };

        //    ReminderFakeRepository reminderFakeRepository = new ReminderFakeRepository(reminders);
        //    ReminderCommandHandler handler = new ReminderCommandHandler(
        //        reminderFakeRepository,
        //        GetMapper(),
        //        GetIdentityService(profileId),
        //        GetMediator(),
        //        GetUnitOfWork(),
        //        GetLogger()
        //        );

        //    Assert.AreEqual(1, reminderFakeRepository._reminders.Count);

        //    CommandResult commandResult = await handler.Handle(command, default(CancellationToken));

        //    Assert.IsTrue(commandResult.Success);
        //    Assert.AreEqual(0, reminderFakeRepository._reminders.Count);
        //}

        //[TestMethod]
        //public async Task ShouldFailToRemoveReminderIdNotFound()
        //{
        //    RemoveReminderCommand command = new RemoveReminderCommand
        //    {
        //        ReminderId = Guid.NewGuid()
        //    };

        //    ReminderFakeRepository reminderFakeRepository = new ReminderFakeRepository();
        //    ReminderCommandHandler handler = new ReminderCommandHandler(
        //        reminderFakeRepository,
        //        GetMapper(),
        //        GetIdentityService(),
        //        GetMediator(),
        //        GetUnitOfWork(),
        //        GetLogger()
        //        );

        //    CommandResult commandResult = await handler.Handle(command, default(CancellationToken));

        //    Assert.IsFalse(commandResult.Success);
        //    Assert.AreEqual("Falha ao buscar Lembrete no banco de dados.", commandResult.Notifications.FirstOrDefault().Description);
        //}

        //[TestMethod]
        //public async Task ShouldFailToRemoveReminderUserNotOwner()
        //{
        //    Guid profileId = Guid.NewGuid();

        //    List<Reminder> reminders = new List<Reminder>
        //    {
        //        new Reminder(profileId, true, "Novo para teste", "Testando", new Time(9, 0, 0))
        //    };

        //    RemoveReminderCommand command = new RemoveReminderCommand
        //    {
        //        ReminderId = reminders.First().Id
        //    };

        //    ReminderFakeRepository reminderFakeRepository = new ReminderFakeRepository(reminders);
        //    ReminderCommandHandler handler = new ReminderCommandHandler(
        //        reminderFakeRepository,
        //        GetMapper(),
        //        GetIdentityService(),
        //        GetMediator(),
        //        GetUnitOfWork(),
        //        GetLogger()
        //        );

        //    CommandResult commandResult = await handler.Handle(command, default(CancellationToken));

        //    Assert.IsFalse(commandResult.Success);
        //    Assert.AreEqual("Falha ao buscar Lembrete no banco de dados.", commandResult.Notifications.FirstOrDefault().Description);
        //}
        //#endregion
    }
}
