using System;

namespace NutrientAuto.Community.Domain.ReadModels.MeasureAggregate
{
    public class MeasureListReadModel
    {
        public Guid Id { get; set; }
        public Guid ProfileId { get; set; }
        public string Title { get; set; }
        public DateTime DateMeasure { get; set; }

        public int BodyPicturesCount { get; set; }
    }
}
