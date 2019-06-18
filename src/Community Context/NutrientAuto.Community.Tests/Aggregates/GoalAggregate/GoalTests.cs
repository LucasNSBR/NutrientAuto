using Microsoft.VisualStudio.TestTools.UnitTesting;
using NutrientAuto.Community.Domain.Aggregates.GoalAggregate;
using System;
using System.Linq;

namespace NutrientAuto.Community.Tests.Aggregates.GoalAggregate
{
    [TestClass]
    public class GoalTests
    {
        #region Initialization
        [TestMethod]
        public void ShouldSetInitialVariables()
        {
            Guid profileId = Guid.NewGuid();
            Goal goal = new Goal(profileId, "Novo objetivo de testes", "Testando a inicialização");

            Assert.AreNotEqual(Guid.Empty, goal.ProfileId);
            Assert.AreEqual("Novo objetivo de testes", goal.Title);
            Assert.AreEqual("Testando a inicialização", goal.Details);
        }

        [TestMethod]
        public void ShouldInitializeDateCreatedProperty()
        {
            Goal goal = new Goal(Guid.NewGuid(), "teste de data", "apenas testando data");

            Assert.AreEqual(DateTime.Now.Year, goal.DateCreated.Year);
            Assert.AreEqual(DateTime.Now.Month, goal.DateCreated.Month);
            Assert.AreEqual(DateTime.Now.Day, goal.DateCreated.Day);
        }

        [TestMethod]
        public void ShouldSetInitialStatusForGoal()
        {
            Goal goal = new Goal(Guid.NewGuid(), "Testando Status", "Testes de Status");

            Assert.IsNotNull(goal.Status);
            Assert.IsFalse(goal.Status.IsCompleted);
            Assert.IsNull(goal.Status.AccomplishmentDetails);
            Assert.IsNull(goal.Status.DateCompleted);
        }
        #endregion

        #region Update
        [TestMethod]
        public void ShouldUpdateVariables()
        {
            Goal goal = new Goal(Guid.NewGuid(), "Testando Update", "Testes de Update");

            goal.Update("Foi feito o update", "Update feito");

            Assert.AreEqual("Foi feito o update", goal.Title);
            Assert.AreEqual("Update feito", goal.Details);
        }
        #endregion

        #region SetCompleted
        [TestMethod]
        public void ShouldSetCompleted()
        {
            DateTime dateCompleted = DateTime.Now;
            Goal goal = new Goal(Guid.NewGuid(), "Testando Completed", "Testes de Completition");

            goal.SetCompleted(dateCompleted, "Deve ser completado com successo!");

            Assert.IsNotNull(goal.Status);
            Assert.IsTrue(goal.Status.IsCompleted);
            Assert.AreEqual("Deve ser completado com successo!", goal.Status.AccomplishmentDetails);
            Assert.AreEqual(dateCompleted, goal.Status.DateCompleted);
        }

        [TestMethod]
        public void ShouldFailToSetCompletedTwiceTimes()
        {
            Goal goal = new Goal(Guid.NewGuid(), "Testando Completed (two times)", "Testes de completition em dobro");

            goal.SetCompleted(DateTime.Now, "Deve ser completado com successo uma única vez!");
            goal.SetCompleted(DateTime.Now, "Deve gerar uma notificação!");

            Assert.IsFalse(goal.IsValid);
            Assert.IsTrue(goal.GetNotifications().Any());
        }

        [TestMethod]
        public void ShouldFailToSetCompletedWithCompletedDateLessThanDateCreated()
        {
            DateTime oneYearEalier = DateTime.Now.AddYears(-1);
            Goal goal = new Goal(Guid.NewGuid(), "Vai falhar", "Não vai ser completado com uma data menor que a data de criação");

            goal.SetCompleted(oneYearEalier, "Vai gerar um erro!");

            Assert.IsFalse(goal.IsValid);
            Assert.IsTrue(goal.GetNotifications().Any());
        }

        [TestMethod]
        public void ShouldCatchBothErrorsAboutCompletition()
        {
            DateTime oneYearEalier = DateTime.Now.AddYears(-1);
            Goal goal = new Goal(Guid.NewGuid(), "Falhar por dois erros", "Vai falhar porque já está completo e a data está menor que a de criação");

            goal.SetCompleted(DateTime.Now, "Deve completar uma vez");
            goal.SetCompleted(oneYearEalier, "Vai gerar um erro!");

            Assert.IsFalse(goal.IsValid);
            Assert.AreEqual(2, goal.GetNotifications().Count);
        }
        #endregion

        #region SetUncompleted 
        [TestMethod]
        public void ShouldSetGoalUncompleted()
        {
            Goal goal = new Goal(Guid.NewGuid(), "Vai completar", "Vai completar para depois usar SetUncompleted");

            goal.SetCompleted(DateTime.Now, "Deve completar e depois ser descompletado");
            goal.SetUncompleted();

            Assert.IsNotNull(goal.Status);
            Assert.IsFalse(goal.Status.IsCompleted);
            Assert.IsNull(goal.Status.AccomplishmentDetails);
            Assert.IsNull(goal.Status.DateCompleted);
        }

        [TestMethod]
        public void ShouldFailToSetCompletedBecauseIsUncompleted()
        {
            Goal goal = new Goal(Guid.NewGuid(), "Não vai ser Uncompleted", "Não pode ser uncompleted duas vezes");

            goal.SetUncompleted();

            Assert.IsFalse(goal.IsValid);
            Assert.IsTrue(goal.GetNotifications().Any());
        }
        #endregion
    }
}
