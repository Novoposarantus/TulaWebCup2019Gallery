using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DtoModels;
using Models.Exceptions;
using System.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : BaseController
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