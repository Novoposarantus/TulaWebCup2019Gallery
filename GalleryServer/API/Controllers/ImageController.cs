using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DtoModels;
using Models.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        readonly IImageRepository _imageRepository;
        public ImageController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        [HttpPost]
        public IActionResult GetImage(FilterIdDto imageData)
        {
            int? userId = null;
            if(User != null)
            {
                userId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value);
            }
            try
            {
                return Ok(_imageRepository.Get(imageData, userId));
            }
            catch (ImageRepositoryException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody]List<ImageDto> image)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value);
            _imageRepository.Save(image, userId);
            return Ok();
        }
    }
}