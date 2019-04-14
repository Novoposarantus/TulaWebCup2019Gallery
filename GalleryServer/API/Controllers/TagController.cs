using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DtoModels;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TagController : BaseController
    {
        readonly IImageRepository _imageRepository;
        public TagController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        [HttpPost]
        public IActionResult Post([FromBody]ImageTagsDto tagsInfo)
        {
            _imageRepository.AddTags(tagsInfo, _userId.Value);
            return Ok();
        }
    }
}