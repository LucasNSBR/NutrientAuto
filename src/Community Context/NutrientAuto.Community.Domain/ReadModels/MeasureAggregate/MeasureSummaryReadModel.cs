using NutrientAuto.Community.Domain.Aggregates.MeasureAggregate;
using NutrientAuto.Shared.ValueObjects;
using System;
using System.Collections.Generic;

namespace NutrientAuto.Community.Domain.ReadModels.MeasureAggregate
{
    public class MeasureSummaryReadModel
    {
        public Guid Id { get; set; }
        public Guid ProfileId { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime DateMeasure { get; set; }

        public BasicMeasure BasicMeasure { get; set; }

        public List<Image> BodyPictures { get; set; }
        private List<MeasureLine> MeasureLines { get; set; }
    }
}
