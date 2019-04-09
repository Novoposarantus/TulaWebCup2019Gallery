using Microsoft.IdentityModel.Tokens;using System.Text;

namespace API.Options
{
    public class AuthenticationOptions
    {
        public const string ISSUER = "http://localhost:61130"; // издатель токена
        public const string AUDIENCE = "http://localhost:61130"; // потребитель токена
        const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
        public const int LIFETIME = 14 * 24; // время жизни токена - 14 дней
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
