using System;

namespace NutrientAuto.Identity.Service.Services.Token.Configuration
{
    public class JwtTokenOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpiresInSeconds { get; set; }
        public DateTime NotBefore { get; set; }
        public DateTime IssuedAt { get; set; }
    }
}
