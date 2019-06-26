using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NutrientAuto.Community.Domain.CommandHandlers.FoodAggregate;
using NutrientAuto.Community.Domain.Commands.FoodAggregate;
using NutrientAuto.Community.Domain.Context;
using NutrientAuto.Community.Domain.Repositories.FoodAggregate;
using NutrientAuto.CrossCutting.HttpService.HttpContext;
using NutrientAuto.CrossCutting.UnitOfwork.Abstractions;
using NutrientAuto.CrossCutting.UnitOfWork;
using NutrientAuto.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Tests.CommandHandlers.FoodAggregate
{
    [TestClass]
    public class FoodCommandHandlerTests
    {
        //#region Arrange
        //public IMediator GetMediator()
        //{
        //    Mock<IMediator> mock = new Mock<IMediator>();
        //    mock.Setup(mediatr => mediatr.Publish(It.IsAny<object>(), It.IsAny<CancellationToken>()))
        //        .Returns(Task.CompletedTask);

        //    return mock.Object;
        //}

        //public IIdentityService GetIdentityService(Guid? id = null)
        //{
        //    Mock<IIdentityService> mock = new Mock<IIdentityService>();
        //    mock.Setup(identity => identity.GetUserId())
        //        .Returns(id ?? Guid.NewGuid());

        //    return mock.Object;
        //}

        //public IUnitOfWork<ICommunityDbContext> GetUnitOfWork()
        //{
        //    Mock<IUnitOfWork<ICommunityDbContext>> mock = new Mock<IUnitOfWork<ICommunityDbContext>>();
        //    mock.Setup(uow => uow.CommitAsync())
        //        .ReturnsAsync(CommitResult.Ok());

        //    return mock.Object;
        //}

        //public ILogger<FoodCommandHandler> GetLogger()
        //{
        //    Mock<ILogger<FoodCommandHandler>> mock = new Mock<ILogger<FoodCommandHandler>>();
        //    mock.Setup(logger => logger.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<object[]>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()));

        //    return mock.Object;
        //}
        //#endregion

        //#region Register
        //[TestMethod]
        //public async Task ShouldRegisterFoodTable()
        //{
        //    RegisterFoodCommand command = new RegisterFoodCommand
        //    {
        //        Name = "Bacon de testes",
        //        Description = "Registrando um bacon de testes",
        //    };

        //    Mock<IFoodRepository> foodTableRepository = new Mock<IFoodRepository>();
        //    FoodCommandHandler handler = new FoodCommandHandler(
        //        foodTableRepository.Object,
        //        GetIdentityService(null),
        //        GetMediator(),
        //        GetUnitOfWork(),
        //        GetLogger()
        //        );

        //    CommandResult commandResult = await handler.Handle(command, default(CancellationToken));

        //    Assert.IsTrue(commandResult.Success);
        //}
        //#endregion

        //#region Update
        //[TestMethod]
        //public async Task ShouldUpdateFoodTable()
        //{
        //    Guid profileId = Guid.NewGuid();
        //    CustomFoodTable existingFoodTable = new CustomFoodTable(profileId, "Bacon inicial", "Bacon antes de sofrer os testes");

        //    Mock<IFoodTableRepository> foodTableRepository = new Mock<IFoodTableRepository>();
        //    foodTableRepository.Setup(repo => repo.GetCustomByIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
        //        .ReturnsAsync(existingFoodTable);

        //    foodTableRepository.Setup(repo => repo.UpdateAsync(It.IsAny<CustomFoodTable>()))
        //        .Callback((CustomFoodTable foodTable) =>
        //        {
        //            existingFoodTable = new CustomFoodTable(foodTable.ProfileId, foodTable.Name, foodTable.Description);
        //        });

        //    UpdateFoodTableCommand command = new UpdateFoodTableCommand
        //    {
        //        FoodTableId = existingFoodTable.Id,
        //        Name = "Bacon após testes",
        //        Description = "Registrando um bacon de testes",
        //    };

        //    CustomFoodTable insertedFoodTable = await foodTableRepository.Object.GetCustomByIdAsync(existingFoodTable.Id, profileId);
        //    Assert.AreEqual("Bacon inicial", insertedFoodTable.Name);

        //    FoodTableCommandHandler handler = new FoodTableCommandHandler(
        //        foodTableRepository.Object,
        //        GetIdentityService(profileId),
        //        GetMediator(),
        //        GetUnitOfWork(),
        //        GetLogger()
        //        );

        //    CommandResult commandResult = await handler.Handle(command, default(CancellationToken));
        //    CustomFoodTable updatedInsertedFoodTable = await foodTableRepository.Object.GetCustomByIdAsync(existingFoodTable.Id, profileId);

        //    Assert.IsTrue(commandResult.Success);
        //    Assert.AreEqual("Bacon após testes", updatedInsertedFoodTable.Name);
        //}

        //[TestMethod]
        //public async Task ShouldFailToUpdateFoodTableIdNotFound()
        //{
        //    UpdateFoodTableCommand command = new UpdateFoodTableCommand
        //    {
        //        FoodTableId = Guid.NewGuid(),
        //        Name = "Bacon de testes",
        //        Description = "Registrando um bacon de testes",
        //    };

        //    Mock<IFoodTableRepository> foodTableRepository = new Mock<IFoodTableRepository>();
        //    foodTableRepository.Setup(repo => repo.GetCustomByIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
        //        .ReturnsAsync(() => null);

        //    FoodTableCommandHandler handler = new FoodTableCommandHandler(
        //        foodTableRepository.Object,
        //        GetIdentityService(),
        //        GetMediator(),
        //        GetUnitOfWork(),
        //        GetLogger()
        //        );

        //    CommandResult commandResult = await handler.Handle(command, default(CancellationToken));

        //    Assert.IsFalse(commandResult.Success);
        //    Assert.AreEqual("Nenhuma categoria de alimentos com esse Id foi encontrada no banco de dados.", commandResult.Notifications.FirstOrDefault().Description);
        //}

        //[TestMethod]
        //public async Task ShouldFailToUpdateFoodTableUserNotOwner()
        //{
        //    Guid profileId = Guid.NewGuid();
        //    CustomFoodTable existingFoodTable = new CustomFoodTable(profileId, "Bacon inicial", "Bacon antes de sofrer os testes");

        //    UpdateFoodTableCommand command = new UpdateFoodTableCommand
        //    {
        //        FoodTableId = existingFoodTable.Id,
        //        Name = "Bacon de testes",
        //        Description = "Registrando um bacon de testes",
        //    };

        //    Mock<IFoodTableRepository> foodTableRepository = new Mock<IFoodTableRepository>();
        //    foodTableRepository.Setup(repo => repo.GetCustomByIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
        //        .ReturnsAsync((Guid id, Guid pid) =>
        //        {
        //            if (pid == existingFoodTable.ProfileId && id == existingFoodTable.Id)
        //                return existingFoodTable;

        //            return null;
        //        });

        //    FoodTableCommandHandler handler = new FoodTableCommandHandler(
        //        foodTableRepository.Object,
        //        GetIdentityService(),
        //        GetMediator(),
        //        GetUnitOfWork(),
        //        GetLogger()
        //        );

        //    CommandResult commandResult = await handler.Handle(command, default(CancellationToken));

        //    Assert.IsFalse(commandResult.Success);
        //    Assert.AreEqual("Nenhuma categoria de alimentos com esse Id foi encontrada no banco de dados.", commandResult.Notifications.FirstOrDefault().Description);
        //}
        //#endregion

        //#region Remove 
        //[TestMethod]
        //public async Task ShouldRemoveFoodTable()
        //{
        //    Guid profileId = Guid.NewGuid();
        //    CustomFoodTable existingFoodTable = new CustomFoodTable(profileId, "Bacon inicial", "Bacon antes de sofrer os testes");
        //    bool isRemoved = false;

        //    Mock<IFoodTableRepository> foodTableRepository = new Mock<IFoodTableRepository>();
        //    foodTableRepository.Setup(repo => repo.GetCustomByIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
        //        .ReturnsAsync(() =>
        //        {
        //            if (!isRemoved)
        //                return existingFoodTable;

        //            return null;
        //        });

        //    foodTableRepository.Setup(repo => repo.RemoveAsync(It.IsAny<CustomFoodTable>()))
        //        .Callback((CustomFoodTable foodTable) =>
        //        {
        //            isRemoved = true;
        //        });

        //    RemoveFoodTableCommand command = new RemoveFoodTableCommand
        //    {
        //        FoodTableId = existingFoodTable.Id,
        //        Name = "Bacon após testes",
        //        Description = "Registrando um bacon de testes",
        //    };

        //    CustomFoodTable insertedFoodTable = await foodTableRepository.Object.GetCustomByIdAsync(existingFoodTable.Id, profileId);
        //    Assert.AreEqual("Bacon inicial", insertedFoodTable.Name);

        //    FoodTableCommandHandler handler = new FoodTableCommandHandler(
        //        foodTableRepository.Object,
        //        GetIdentityService(profileId),
        //        GetMediator(),
        //        GetUnitOfWork(),
        //        GetLogger()
        //        );

        //    CommandResult commandResult = await handler.Handle(command, default(CancellationToken));
        //    CustomFoodTable updatedInsertedFoodTable = await foodTableRepository.Object.GetCustomByIdAsync(Guid.NewGuid(), profileId);

        //    Assert.IsTrue(commandResult.Success);
        //    Assert.IsNull(updatedInsertedFoodTable);
        //}

        //[TestMethod]
        //public async Task ShouldFailToRemoveFoodTableIdNotFound()
        //{
        //    RemoveFoodTableCommand command = new RemoveFoodTableCommand
        //    {
        //        FoodTableId = Guid.NewGuid()
        //    };

        //    Mock<IFoodTableRepository> foodTableRepository = new Mock<IFoodTableRepository>();
        //    foodTableRepository.Setup(repo => repo.GetCustomByIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
        //        .ReturnsAsync(() => null);

        //    FoodTableCommandHandler handler = new FoodTableCommandHandler(
        //        foodTableRepository.Object,
        //        GetIdentityService(),
        //        GetMediator(),
        //        GetUnitOfWork(),
        //        GetLogger()
        //        );

        //    CommandResult commandResult = await handler.Handle(command, default(CancellationToken));

        //    Assert.IsFalse(commandResult.Success);
        //    Assert.AreEqual("Nenhuma categoria de alimentos com esse Id foi encontrada no banco de dados.", commandResult.Notifications.FirstOrDefault().Description);
        //}

        //[TestMethod]
        //public async Task ShouldFailToRemoveFoodTableUserNotOwner()
        //{
        //    Guid profileId = Guid.NewGuid();
        //    CustomFoodTable existingFoodTable = new CustomFoodTable(profileId, "Bacon inicial", "Bacon antes de sofrer os testes");

        //    RemoveFoodTableCommand command = new RemoveFoodTableCommand
        //    {
        //        FoodTableId = existingFoodTable.Id
        //    };

        //    Mock<IFoodTableRepository> foodTableRepository = new Mock<IFoodTableRepository>();
        //    foodTableRepository.Setup(repo => repo.GetCustomByIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
        //        .ReturnsAsync((Guid id, Guid pid) =>
        //        {
        //            if (pid == existingFoodTable.ProfileId && id == existingFoodTable.Id)
        //                return existingFoodTable;

        //            return null;
        //        });

        //    FoodTableCommandHandler handler = new FoodTableCommandHandler(
        //        foodTableRepository.Object,
        //        GetIdentityService(),
        //        GetMediator(),
        //        GetUnitOfWork(),
        //        GetLogger()
        //        );

        //    CommandResult commandResult = await handler.Handle(command, default(CancellationToken));

        //    Assert.IsFalse(commandResult.Success);
        //    Assert.AreEqual("Nenhuma categoria de alimentos com esse Id foi encontrada no banco de dados.", commandResult.Notifications.FirstOrDefault().Description);
        //}
        //#endregion
    }
}
