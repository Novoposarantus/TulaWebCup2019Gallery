using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DtoModels;
using Models.Exceptions;
using System.Linq;
using System.Security.Claims;

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
            int? userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value);
            }
            try
            {
                var images = _imageRepository.Get(filter, userId);
                return Ok(new { images, imagesCount = _imageRepository.GetImageCount()});
            }
            catch (ImageRepositoryException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}