using System.Collections.Generic;
using GodelTech.Owasp.Web.Helpers;
using GodelTech.Owasp.Web.Models;
using GodelTech.Owasp.Web.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GodelTech.Owasp.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class StoreController : ControllerBase
    {
        private readonly IAlbumRepository _repository;

        public StoreController(IAlbumRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public Album Album(string id)
        {
            return _repository.Get(id);
        }

        [HttpGet("{searchKey}")]
        public IEnumerable<Album> Search(string searchKey)
        {
            return _repository.GetList(searchKey);
        }

        [HttpGet("{genreId:int}")]
        public IEnumerable<Album> Albums(int genreId)
        {
            return _repository.GetList(genreId);
        }

        [HttpGet("{skip:int}/{take:int}")]
        public IEnumerable<Album> Page(int skip, int take)
        {
            return _repository.GetList(skip, take);
        }
    }
}