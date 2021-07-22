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
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository _repository;

        public GenreController(IGenreRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<Genre> All()
        {
            return _repository.Get();
        }
    }
}