using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DtoModels;
using Models.Exceptions;

namespace API.Controllers
{
    public class ImageController : Controller
    {
        readonly IImageRepository _imageRepository;
        public ImageController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        public IActionResult Get(FilterDto filter)
        {
            try
            {
                return Ok(_imageRepository.GetImages(filter));
            }
            catch(ImageRepositoryException e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}