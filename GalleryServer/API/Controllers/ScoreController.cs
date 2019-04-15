using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DtoModels;
using Models.Exceptions;
using System.Linq;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ScoreController : ControllerBase
    {
        readonly IImageRepository _imageRepository;
        public ScoreController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        [HttpPost]
        public IActionResult Post([FromBody]ImageScoreDto scoreInfo)
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value);
                return Ok(_imageRepository.AddScore(scoreInfo.ScoreValue, scoreInfo.ImageId, userId));
            }
            catch (ImageRepositoryException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}