using NutrientAuto.Community.Domain.ReadModels.MeasureAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Domain.Repositories.MeasureAggregate
{
    public interface IMeasureReadModelRepository
    {
        Task<IEnumerable<MeasureListReadModel>> GetMeasureListAsync(Guid profileId, string titleFilter = null, int pageNumber = 1, int pageSize = 20);
        Task<MeasureSummaryReadModel> GetMeasureSummaryAsync(Guid id);
    }
}
