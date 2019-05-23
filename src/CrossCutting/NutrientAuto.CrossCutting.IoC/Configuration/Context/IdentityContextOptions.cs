using Microsoft.AspNetCore.Identity;

namespace NutrientAuto.CrossCutting.IoC.Configuration.Context
{
    public class IdentityContextOptions
    {
        public UserOptions UserOptions { get; set; }
        public PasswordOptions PasswordOptions { get; set; }
        public SignInOptions SignInOptions { get; set; }
    }
}
