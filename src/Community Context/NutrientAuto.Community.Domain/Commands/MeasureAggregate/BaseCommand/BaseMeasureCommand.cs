using MediatR;
using NutrientAuto.Community.Domain.Commands.SeedWork;
using NutrientAuto.Shared.Commands;
using System;
using System.Collections.Generic;

namespace NutrientAuto.Community.Domain.Commands.MeasureAggregate.BaseCommand
{
    public abstract class BaseMeasureCommand : Command, IRequest<CommandResult>
    {
        public Guid MeasureId { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }

        public decimal Height { get; set; }
        public decimal Weight { get; set; }

        public DateTime MeasureDate { get; set; }
        public List<ImageDto> BodyPictures { get; set; } = new List<ImageDto>();

        public bool WritePost { get; set; }
        public List<MeasureLineDto> MeasureLines { get; set; } = new List<MeasureLineDto>();
    }
}
