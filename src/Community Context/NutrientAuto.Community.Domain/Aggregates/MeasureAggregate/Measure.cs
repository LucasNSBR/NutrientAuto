using NutrientAuto.Community.Domain.Aggregates.SeedWork;
using NutrientAuto.Shared.Entities;
using NutrientAuto.Shared.ValueObjects;
using System;
using System.Collections.Generic;

namespace NutrientAuto.Community.Domain.Aggregates.MeasureAggregate
{
    public class Measure : Entity<Measure>, IAggregateRoot, IProfileEntity
    {
        public Guid ProfileId { get; private set; }

        public string Title { get; private set; }
        public string Details { get; private set; }
        public BasicMeasure BasicMeasure { get; private set; }
        public DateTime MeasureDate { get; private set; }

        private List<Image> _bodyPictures = new List<Image>();
        public IReadOnlyList<Image> BodyPictures => _bodyPictures;

        private List<MeasureLine> _measureLines = new List<MeasureLine>();
        public IReadOnlyList<MeasureLine> MeasureLines => _measureLines;

        protected Measure()
        {
        }

        public Measure(Guid profileId, string title, string details, BasicMeasure basicMeasure, DateTime measureDate, List<Image> bodyPictures, List<MeasureLine> measureLines)
        {
            ProfileId = profileId;
            Title = title;
            Details = details;
            BasicMeasure = basicMeasure;
            MeasureDate = measureDate;
            _bodyPictures = bodyPictures;
            _measureLines = measureLines;
        }

        public void Update(string title, string details, BasicMeasure basicMeasure, DateTime measureDate, List<Image> bodyPictures, List<MeasureLine> measureLines)
        {
            Title = title;
            Details = details;
            BasicMeasure = basicMeasure;
            MeasureDate = measureDate;
            _bodyPictures = bodyPictures;
            _measureLines = measureLines;
        }
    }
}
