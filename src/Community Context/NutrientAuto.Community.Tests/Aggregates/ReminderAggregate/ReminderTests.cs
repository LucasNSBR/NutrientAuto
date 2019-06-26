using Microsoft.VisualStudio.TestTools.UnitTesting;
using NutrientAuto.Community.Domain.Aggregates.ReminderAggregate;
using NutrientAuto.Shared.ValueObjects;
using System;

namespace NutrientAuto.Community.Tests.Aggregates.ReminderAggregate
{
    [TestClass]
    public class ReminderTests
    {
        #region Initialization
        [TestMethod]
        public void ShouldSetInitialReminderVariables()
        {
            Reminder reminder = new Reminder(Guid.NewGuid(), true, "Almoço", "Hora do almoço", new Time(10, 0, 0));

            Assert.AreNotEqual(Guid.Empty, reminder.ProfileId);
            Assert.IsTrue(reminder.Active);
            Assert.AreEqual("Almoço", reminder.Title);
            Assert.AreEqual("Hora do almoço", reminder.Details);
            Assert.AreEqual(new Time(10, 0, 0), reminder.TimeOfDay);
        }
        #endregion

        #region Update
        [TestMethod]
        public void ShouldUpdateReminderAggregate()
        {
            Reminder reminder = new Reminder(Guid.NewGuid(), true, "Almoço", "Hora do almoço", new Time(10, 0, 0));

            reminder.Update(false, "Mudou para o jantar", "Jantar", new Time(12, 0, 0));

            Assert.IsFalse(reminder.Active);
            Assert.AreEqual("Mudou para o jantar", reminder.Title);
            Assert.AreEqual("Jantar", reminder.Details);
            Assert.AreEqual(new Time(12, 0, 0), reminder.TimeOfDay);
        }
        #endregion
    }
}
