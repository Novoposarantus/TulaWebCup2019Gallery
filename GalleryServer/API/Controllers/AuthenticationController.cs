using System;
using System.Threading.Tasks;
using Models.DtoModels;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Domain.Helpers;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using API.Options;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        readonly IUserRepository _userRepository;
        public AuthenticationController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpPost]
        public IActionResult Post([FromBody] AuthenticationModel model)
        {
            var user = _userRepository.GetUser(model.UserName, model.Password);
            if (user == null)
            {
                return Unauthorized("Неверный логин или пароль");
            }
            var identity = AuthenticationHelper.GetIdentity(user, model.Password);

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: AuthenticationOptions.ISSUER,
                    audience: AuthenticationOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity,
                    expires: now.Add(TimeSpan.FromMinutes(AuthenticationOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthenticationOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                timeOut = AuthenticationOptions.LIFETIME,
                userName = user.UserName,
                roleId = user.Role.Id
            };
            return Ok(response);
        }
    }
}