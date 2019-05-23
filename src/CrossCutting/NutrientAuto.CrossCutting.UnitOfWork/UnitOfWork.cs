using Microsoft.EntityFrameworkCore;
using NutrientAuto.CrossCutting.UnitOfwork.Abstractions;
using NutrientAuto.CrossCutting.UnitOfWork.Exceptions;
using System;
using System.Threading.Tasks;

namespace NutrientAuto.CrossCutting.UnitOfWork
{
    public class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext> 
                            where TDbContext : IDbContext<TDbContext>
    {
        private readonly IDbContext<TDbContext> _dbContext;

        public UnitOfWork(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CommitResult> CommitAsync()
        {
            try
            {
                int modifiedRows = await _dbContext.SaveChangesAsync();

                if (modifiedRows > 0)
                    return CommitResult.Ok();
                else
                    throw new DataNotModifiedException();
            }
            catch(DataNotModifiedException dnmex)
            {
                return CommitResult.Failure(dnmex);
            }
            catch (DbUpdateException dbex)
            {
                return CommitResult.Failure(dbex);
            }
            catch (InvalidOperationException ioex)
            {
                return CommitResult.Failure(ioex);
            }
            catch (NotSupportedException nsex)
            {
                return CommitResult.Failure(nsex);
            }
            catch (Exception ex)
            {
                return CommitResult.Failure(ex);
            }
        }

        public void Rollback()
        {
            //EF Rollback's are automatic
        }
    }
}
