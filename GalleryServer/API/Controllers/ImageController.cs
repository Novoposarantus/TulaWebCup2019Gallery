using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DtoModels;
using Models.Exceptions;

namespace API.Controllers
{
    public class ImageController : BaseController
    {
        readonly IImageRepository _imageRepository;
        public ImageController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

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

        public IActionResult Get([FromBody]FilterDto filter)
        {
            try
            {
                return Ok(_imageRepository.Get(filter));
            }
            catch(ImageRepositoryException e)
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

        [HttpPost]
        [Authorize]
        public IActionResult SaveTags([FromBody]ImageTagsDto tagsInfo)
        {
            _imageRepository.AddTags(tagsInfo, _userId.Value);
            return Ok();
        }

        [HttpPost]
        [Authorize]
        public IActionResult SaveScore([FromBody]ImageScoreDto scoreInfo)
        {
            try
            {
                _imageRepository.AddScore(scoreInfo.ScoreValue, scoreInfo.ImageId, _userId.Value);
                return Ok();
            }
            catch(ImageRepositoryException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}