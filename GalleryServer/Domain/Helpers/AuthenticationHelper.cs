using Models.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Helpers
{
    public static class AuthenticationHelper
    {
        public static IReadOnlyCollection<Claim> GetIdentity(User user, string password)
        {
            List<Claim> claims = null;
            if (user != null)
            {
                var passwordHash = HashPassword(password);

                if (passwordHash == user.Password)
                {
                    claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.Role.Name),
                        new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName)
                    };
                }
            }
            return claims;
        }
        public static string HashPassword(string password)
        {
            var sha256 = new SHA256Managed();
            return Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }
    }
}
