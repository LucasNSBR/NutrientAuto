using System;

namespace NutrientAuto.CrossCutting.UnitOfWork.Exceptions
{
    [Serializable]
    public class DataNotModifiedException : Exception
    {
        public DataNotModifiedException() : base("A operação não conseguiu modificar nenhum registro no banco de dados.")
        {
        }
    }
}
