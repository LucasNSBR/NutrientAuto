using Microsoft.VisualStudio.TestTools.UnitTesting;
using NutrientAuto.Community.Domain.Aggregates.MeasureAggregate;
using System;

namespace NutrientAuto.Community.Tests.Aggregates.MeasureAggregate
{
    [TestClass]
    public class MeasureTests
    {
        #region Arrange
        public Measure GetMeasure()
        {
            Guid profileId = Guid.NewGuid();
            Measure measure = new Measure(profileId, "Nova medição", "Nova medição - Detalhes", new BasicMeasure(169, 65), DateTime.Now, null, null);
            return measure;
        }
        #endregion

        #region Initialization
        [TestMethod]
        public void ShouldSetInitialMeasureVariables()
        {
            Measure measure = GetMeasure();

            Assert.AreNotEqual(Guid.Empty, measure.ProfileId);
            Assert.AreEqual("Nova medição", measure.Title);
            Assert.AreEqual("Nova medição - Detalhes", measure.Details);
            Assert.AreEqual(new BasicMeasure(169, 65), measure.BasicMeasure);
        }
        #endregion

        #region Update
        [TestMethod]
        public void ShouldUpdateMeasureAggregate()
        {
            Measure measure = GetMeasure();

            measure.Update("Update de título", "Update de detalhes", new BasicMeasure(200, 100), DateTime.Now.AddYears(1), null, null);

            Assert.AreNotEqual(Guid.Empty, measure.ProfileId);
            Assert.AreEqual("Update de título", measure.Title);
            Assert.AreEqual("Update de detalhes", measure.Details);
            Assert.AreEqual(new BasicMeasure(200, 100), measure.BasicMeasure);
        }
        #endregion
    }
}
