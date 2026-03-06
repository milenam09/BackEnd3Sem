using Microsoft.IdentityModel.Tokens;

namespace Microsoft.Identity
{
    internal class Tokens
    {
        internal class TokensValidationParameters : TokenValidationParameters
        {
            public bool ValidatedIssuer { get; set; }
            public bool ValidateAudience { get; set; }
            public bool ValidateLifetime { get; set; }
            public SymmetricSecurityKey IssuerSigniKey { get; set; }
            public TimeSpan ClockSkew { get; set; }
            public string ValidIssuer { get; set; }
            public string ValidAudience { get; set; }
        }
    }
}