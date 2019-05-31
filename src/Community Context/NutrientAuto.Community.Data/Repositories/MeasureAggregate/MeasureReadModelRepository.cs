using Dapper;
using Microsoft.EntityFrameworkCore;
using NutrientAuto.Community.Data.Context;
using NutrientAuto.Community.Domain.Aggregates.MeasureAggregate;
using NutrientAuto.Community.Domain.ReadModels.MeasureAggregate;
using NutrientAuto.Community.Domain.Repositories.MeasureAggregate;
using NutrientAuto.Shared.ValueObjects;
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

        public async Task<IEnumerable<MeasureListReadModel>> GetMeasureListAsync(Guid profileId, string titleFilter = null, int pageNumber = 1, int pageSize = 20)
        {
            string sql = $@"SELECT Measures.Id, Measures.ProfileId, Measures.Title, Measures.MeasureDate, 
                         (SELECT COUNT(MeasureBodyPictures.Id) FROM MeasureBodyPictures WHERE MeasureBodyPictures.MeasureId = Measures.Id) AS BodyPicturesCount 
                         FROM Measures
                         WHERE Measures.Title LIKE '%{titleFilter ?? string.Empty}%' AND Measures.ProfileId = @profileId
                         ORDER BY Measures.MeasureDate DESC
                         OFFSET (@pageNumber - 1) * @pageSize ROWS
                         FETCH NEXT @pageSize ROWS ONLY";

            using (DbConnection connection = _dbContext.Database.GetDbConnection())
            {
                return await connection
                    .QueryAsync<MeasureListReadModel>(sql, new { profileId, titleFilter = titleFilter ?? string.Empty, pageNumber, pageSize });
            }
        }

        public async Task<MeasureSummaryReadModel> GetMeasureSummaryAsync(Guid id)
        {
            string sql = @"SELECT Measures.Id, Measures.ProfileId, Measures.Title, Measures.Details, Measures.MeasureDate, 
                         Measures.Height AS Height, Measures.Weight AS Weight, Measures.Bmi AS Bmi,
                         MeasureBodyPictures.BodyPictureImageName AS ImageName, MeasureBodyPictures.BodyPictureImageUrlPath AS UrlPath,
                         MeasureLines.Id, MeasureLines.MeasureCategoryId, MeasureLines.MeasureId, MeasureLines.Value, MeasureLines.Name, MeasureLines.Description
                         FROM Measures
                         LEFT JOIN MeasureBodyPictures ON MeasureBodyPictures.MeasureId = Measures.Id
                         LEFT JOIN (SELECT MeasureLines.Id, MeasureLines.MeasureId, MeasureLines.MeasureCategoryId AS MeasureCategoryId, MeasureLines.Value AS Value, MeasureCategories.Name, MeasureCategories.Description FROM MeasureLines INNER JOIN MeasureCategories ON MeasureCategoryId = MeasureCategories.Id) MeasureLines ON MeasureLines.MeasureId = Measures.Id
                         WHERE Measures.Id = @id;";

            using (DbConnection connection = _dbContext.Database.GetDbConnection())
            {
                Dictionary<Guid, MeasureSummaryReadModel> rows = new Dictionary<Guid, MeasureSummaryReadModel>();

                return (await connection
                    .QueryAsync<MeasureSummaryReadModel, BasicMeasure, Image, MeasureLineReadModel, MeasureSummaryReadModel>(sql,
                    (measure, basicMeasure, bodyPicture, measureLine) =>
                    {
                        MeasureSummaryReadModel summary;

                        if (!rows.TryGetValue(id, out summary))
                        {
                            summary = measure;
                            summary.BodyPictures = new List<Image>();
                            summary.MeasureLines = new List<MeasureLineReadModel>();
                            rows.Add(id, summary);
                        }

                        if (bodyPicture != null)
                        {
                            if (!summary.BodyPictures.Contains(bodyPicture))
                                summary.BodyPictures.Add(bodyPicture);
                        }

                        if (measureLine != null)
                        {
                            if (!summary.MeasureLines.Any(m => m.Id == measureLine.Id))
                                summary.MeasureLines.Add(measureLine);
                        }

                        measure.BasicMeasure = basicMeasure;
                        return measure;
                    },
                    new { id },
                    splitOn: "Height,ImageName,Id"))
                    .FirstOrDefault();
            }
        }
    }
}
