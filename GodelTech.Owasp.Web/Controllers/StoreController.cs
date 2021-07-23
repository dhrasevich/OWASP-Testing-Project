// using GodelTech.Owasp.Web.Helpers;
using GodelTech.Owasp.Web.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GodelTech.Owasp.Web.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class StoreController : ControllerBase
    {
        private readonly IAlbumRepository _albumRepository;

        public StoreController(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        [HttpGet("{id}")]
        public IActionResult Album(string id)
        {
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