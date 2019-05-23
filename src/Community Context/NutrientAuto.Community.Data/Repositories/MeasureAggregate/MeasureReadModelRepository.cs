using Dapper;
using Microsoft.EntityFrameworkCore;
using NutrientAuto.Community.Data.Context;
using NutrientAuto.Community.Domain.Aggregates.MeasureAggregate;
using NutrientAuto.Community.Domain.ReadModels.MeasureAggregate;
using NutrientAuto.Community.Domain.Repositories.MeasureAggregate;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Data.Repositories.MeasureAggregate
{
    public class MeasureReadModelRepository : BaseReadModelRepository, IMeasureReadModelRepository
    {
        public MeasureReadModelRepository(CommunityDbContext dbContext)
            : base(dbContext)
        {
        }

        public Task<IEnumerable<MeasureListReadModel>> GetMeasureListAsync(Guid profileId, string titleFilter = null, int pageNumber = 1, int pageSize = 20)
        {
            string sql = @"SELECT Measures.Id, Measures.ProfileId, Measures.Title, Measures.DateMeasure, COUNT(BodyPictures.Id) as BodyPicturesCount FROM Measures
                         LEFT JOIN BodyPictures ON BodyPictures.Id = Measures.Id
                         WHERE Title LIKE %@titleFilter%
                         ORDER BY DateCreated DESC
                         GROUP BY Id
                         OFFSET (@pageNumber - 1) * @pageSize ROWS
                         FETCH NEXT @pageSize ROWS ONLY";

            return GetAllAsync<MeasureListReadModel>(sql, new { profileId, titleFilter = titleFilter ?? string.Empty, pageNumber, pageSize });
        }

        public async Task<MeasureSummaryReadModel> GetMeasureSummaryAsync(Guid id)
        {
            string sql = @"SELECT Measures.Id, Measures.ProfileId, Measures.Title, Measures.Details, Measures.DateMeasure 
                         Measures.Height as Height, Measures.Weight as Weight, Measures.Bmi as Bmi FROM Measures
                         LEFT JOIN BodyPictures ON BodyPictures.Id = Measures.Id
                         WHERE Id = @id";

            using (DbConnection connection = _dbContext.Database.GetDbConnection())
            {
                return (await connection
                    .QueryAsync<MeasureSummaryReadModel, BasicMeasure, MeasureSummaryReadModel>(sql,
                    (measure, basicMeasure) =>
                    {
                        measure.BasicMeasure = basicMeasure;
                        return measure;
                    },
                    new { id },
                    splitOn: "Height"))
                    .FirstOrDefault();
            }
        }
    }
}
