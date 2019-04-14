using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DtoModels;
using Models.Exceptions;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ScoreController : BaseController
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
                _imageRepository.AddScore(scoreInfo.ScoreValue, scoreInfo.ImageId, _userId.Value);
                return Ok();
            }
            catch (ImageRepositoryException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}