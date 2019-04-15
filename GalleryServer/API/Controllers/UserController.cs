using Models.DtoModels;
using Models.Enums;
using Models.Exceptions;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [Authorize]
        public IActionResult Get()
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value);
                var user = _userRepository.GetUser(userId);
                return Ok(new UserDto(user));
            }
            catch (UserRepositoryException e)
            {
                return Unauthorized(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]RegistrationDto model)
        {
            if(model.Password != model.PasswordConfirm)
            {
                return Unauthorized("Пароли не совпадают");
            }
            try
            {
                _userRepository.SaveNewUser(new UserModel(model.UserName, model.Password, (int)RoleEnum.User));
            }
            catch (UserRepositoryException e)
            {
                return Unauthorized(e.Message);
            }
            return Ok();
        }
    }
}