using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;

namespace OpenHumanTask.Runtime.IntegrationTests
{

    internal static class Users
    {

        internal static readonly ClaimsPrincipal Admin = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new(JwtClaimTypes.Subject, Guid.NewGuid().ToString()),
            new(JwtClaimTypes.Name, "fake-admin"),
            new(JwtClaimTypes.Email, "fake-admin@fake.email"),
            new(JwtClaimTypes.Audience, JwtTokenFactory.Issuer)
        }, JwtBearerDefaults.AuthenticationScheme, JwtClaimTypes.Name, JwtClaimTypes.Role));

    }

}
