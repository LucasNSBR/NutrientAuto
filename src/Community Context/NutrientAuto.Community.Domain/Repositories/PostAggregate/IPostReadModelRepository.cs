using NutrientAuto.Community.Domain.ReadModels.PostAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Domain.Repositories.PostAggregate
{
    public interface IPostReadModelRepository
    {
        Task<IEnumerable<PostListReadModel>> GetPostListAsync(Guid profileId, string titleFilter = null, int pageNumber = 1, int pageSize = 20);
        Task<PostSummaryReadModel> GetPostSummaryAsync(Guid id);
    }
}
