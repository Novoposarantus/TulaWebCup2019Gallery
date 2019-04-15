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
    [ApiController]
    public class ImageController : ControllerBase
    {
        readonly IImageRepository _imageRepository;
        public ImageController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        [HttpPost]
        [Route("api/Image/GetImage")]
        public IActionResult GetImage(FilterIdDto imageData)
        {
            int? userId = null;
            if(User.Identity.IsAuthenticated)
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
        [Route("api/Image/GetNext")]
        public IActionResult GetNext(FilterIdDto imageData)
        {
            int? userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value);
            }
            try
            {
                return Ok(_imageRepository.GetNext(imageData, userId));
            }
            catch (ImageRepositoryException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("api/Image/GetPrev")]
        public IActionResult GetPrev(FilterIdDto imageData)
        {
            int? userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value);
            }
            try
            {
                return Ok(_imageRepository.GetPrev(imageData, userId));
            }
            catch (ImageRepositoryException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Authorize]
        [Route("api/Image")]
        public IActionResult Put([FromBody]List<ImageDto> image)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value);
            _imageRepository.Save(image, userId);
            return Ok();
        }
    }
}