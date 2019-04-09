using System.Threading.Tasks;
using Models.DtoModels;
using Models.Enums;
using Models.Exceptions;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Models;

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
        public async Task<IActionResult> Post([FromBody]RegistrationModel model)
        {
            if(model.Password != model.PasswordConfirm)
            {
                return Unauthorized("Пароли не совпадают");
            }
            try
            {
                await _userRepository.SaveNewUser(new User(model.UserName, model.Password, (int)RoleEnum.User));
            }
            catch (RegistrationException e)
            {
                return Unauthorized(e.Message);
            }
            return Ok();
        }
    }
}