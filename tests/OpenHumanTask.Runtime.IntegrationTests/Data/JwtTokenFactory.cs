using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace OpenHumanTask.Runtime.IntegrationTests.Data
{
    internal static class JwtTokenFactory
    {

        internal const string Issuer = "fake-jwt-issuer";
        static readonly byte[] IssuerSigningKeyBytes = new byte[32]; 

        static JwtTokenFactory()
        {
            RandomNumberGenerator.Create().GetBytes(IssuerSigningKeyBytes);
            IssuerSigningKey = new SymmetricSecurityKey(IssuerSigningKeyBytes) { KeyId = Guid.NewGuid().ToString() };
            SigningCredentials = new SigningCredentials(IssuerSigningKey, SecurityAlgorithms.HmacSha256);
        }

        internal static SecurityKey IssuerSigningKey { get; }

        static SigningCredentials SigningCredentials { get; }

        static readonly JwtSecurityTokenHandler _tokenHandler = new();

        internal static string GenerateFor(ClaimsPrincipal user)
        {
            return _tokenHandler.WriteToken(new JwtSecurityToken(Issuer, null, user.Claims, null, DateTime.UtcNow.AddMinutes(20), SigningCredentials));
        }

    }

}
