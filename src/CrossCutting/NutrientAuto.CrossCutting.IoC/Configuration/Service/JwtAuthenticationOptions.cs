using System;

namespace NutrientAuto.CrossCutting.IoC.Configuration.Service
{
    public class JwtAuthenticationOptions
    {
        public bool ValidateAudience { get; set; }
        public bool ValidateIssuer { get; set; } 
        public bool ValidateIssuerSigningKey { get; set; } 
        public bool ValidateLifetime { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpiresInSeconds { get; set; }
        public DateTime NotBefore { get; set; }
        public DateTime IssuedAt { get; set; }
        public TimeSpan ClockSkew { get; set; }
        public string SALT_KEY { get; set; }
    }
}
