// using GodelTech.Owasp.Web.Helpers;
using GodelTech.Owasp.Web.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GodelTech.Owasp.Web.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class StoreController : ControllerBase
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly ILogger<StoreController> _logger;

        public StoreController(IAlbumRepository albumRepository, ILogger<StoreController> logger)
        {
            _albumRepository = albumRepository;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public IActionResult Album(string id)
        {
            _logger.LogWarning($"Getting album by ID {id}");
            return Ok(_albumRepository.Get(id));
        }

        [HttpGet("search/{searchKey}")]
        public IActionResult Search(string searchKey)
        {
            return Ok(_albumRepository.GetList(searchKey));
        }

        [HttpGet("genre/{genreId:int}")]
        public IActionResult Albums(int genreId)
        {
            return Ok(_albumRepository.GetList(genreId));
        }

        [HttpGet("search/{skip:int}/{take:int}")]
        public IActionResult Page(int skip, int take)
        {
            return Ok(_albumRepository.GetList(skip, take));
        }
    }
}