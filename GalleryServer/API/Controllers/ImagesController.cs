using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DtoModels;
using Models.Exceptions;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        readonly IImageRepository _imageRepository;
        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        [HttpPost]
        public IActionResult Post([FromBody]FilterDto filter)
        {
            try
            {
                var images = _imageRepository.Get(filter);
                return Ok(images);
            }
            catch (ImageRepositoryException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}