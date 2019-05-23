using Microsoft.AspNetCore.Identity;
using NutrientAuto.Shared.Entities;
using System;

namespace NutrientAuto.Identity.Domain.Aggregates.UserAggregate
{
    public class NutrientIdentityUser : IdentityUser<Guid>, IAggregateRoot
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }

        protected NutrientIdentityUser()
        {
        }

        public NutrientIdentityUser(string name, string email, DateTime birthDate)
        {
            Name = name;
            Email = email;
            UserName = email;
            BirthDate = birthDate;
        }
    }
}
