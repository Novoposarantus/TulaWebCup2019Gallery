using System;
using System.Threading.Tasks;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Domain.Helpers;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using API.Options;
using Models.Models;
using Models.Exceptions;
using Models.DtoModels;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        readonly IUserRepository _userRepository;
        public AuthenticationController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpPost]
        public IActionResult Post([FromBody] AuthenticationDto model)
        {
            UserModel user;
            try
            {
                user = _userRepository.GetUser(model.UserName, model.Password);
            }
            catch(UserRepositoryException e)
            {
                return Unauthorized(e.Message);
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