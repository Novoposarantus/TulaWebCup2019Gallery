﻿using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DtoModels;
using Models.Exceptions;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : BaseController
    {
        readonly IImageRepository _imageRepository;
        public ImageController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        [Route("api/[controller]/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_imageRepository.Get(id));
            }
            catch (ImageRepositoryException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody]ImageDto image)
        {
            _imageRepository.Save(image, _userId.Value);
            return Ok();
        }
    }
}