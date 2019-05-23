using System;

namespace NutrientAuto.CrossCutting.UnitOfWork
{
    public class CommitResult
    {
        public bool Success { get; }
        public Exception Exception { get; }
        
        private CommitResult()
        {
            Success = true;
        }

        private CommitResult(Exception exception)
        {
            Success = false;
            Exception = exception;
        }

        public static CommitResult Ok()
        {
            return new CommitResult();
        }

        public static CommitResult Failure(Exception exception)
        {
            return new CommitResult(exception);
        }
    }
}
