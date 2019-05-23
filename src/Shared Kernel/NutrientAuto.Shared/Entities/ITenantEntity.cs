using System;

namespace NutrientAuto.Shared.Entities
{
    public interface ITenantEntity
    {
        Guid TenantId { get; }
        void SetTenantId(Guid id);
    }
}
