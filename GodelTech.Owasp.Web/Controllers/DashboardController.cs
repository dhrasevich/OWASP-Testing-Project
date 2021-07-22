using System.Collections.Generic;
using System.IO;
using System.Linq;
using GodelTech.Owasp.Web.Helpers;
using GodelTech.Owasp.InsecureDeserialization.Implementations;
using GodelTech.Owasp.InsecureDeserialization.Interfaces;
using GodelTech.Owasp.Web.Models;
using GodelTech.Owasp.Web.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GodelTech.Owasp.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IAlbumRepository _albumRepository;

        private readonly IInsecureDeserializer<string, IEnumerable<Album>> _binaryFormatterDeserializer;
        private readonly IInsecureDeserializer<string, IEnumerable<AlbumWithEntryPoint>> _newtonSoftJsonDeserializer;

        public DashboardController(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
            _binaryFormatterDeserializer = new InsecureBinaryDeserializer<IEnumerable<Album>>();
            _newtonSoftJsonDeserializer = new InsecureNewtonSoftJsonDeserializer<IEnumerable<AlbumWithEntryPoint>>();
        }

        [HttpGet]
        public IEnumerable<Album> Import()
        {
            return _albumRepository.GetList(0, int.MaxValue);
        }

        //
        // [HttpPost("ImportFromBinaryFile")]
        // public ActionResult ImportFromBinaryFile(IFormFile file)
        // {
        //     try
        //     {
        //         ViewBag.Message = ImportAlbums(file, _binaryFormatterDeserializer);
        //         return View("Import");
        //     }
        //     catch
        //     {
        //         ViewBag.Message = "Albums import failed";
        //         return View("Import");
        //     }
        // }
        //
        // [HttpPost("ImportFromJsonFile")]
        // public ActionResult ImportFromJsonFile(IFormFile file)
        // {
        //     try
        //     {
        //         ViewBag.Message = ImportAlbums(file, _newtonSoftJsonDeserializer);
        //         return View("Import");
        //     }
        //     catch
        //     {
        //         ViewBag.Message = "Albums import failed";
        //         return View("Import");
        //     }
        // }
        //
        // private string ImportAlbums(
        //     IFormFile file,
        //     IInsecureDeserializer<string, IEnumerable<Album>> fileDeserializer)
        // {
        //     if (!IsFileProvided(file))
        //     {
        //         return "Albums were not imported. File is not provided or empty";
        //     }
        //
        //     var albums = GetAlbumsWithImageFromFile(file.InputStream, fileDeserializer);
        //
        //     var enumerable = albums.ToList();
        //
        //     if (!IsAlbumsRetrievedFromFile(enumerable))
        //     {
        //         return "Albums were not imported. File does not contain albums";
        //     }
        //
        //     var numberOfImportedAlbums = _albumRepository.AddIfNotExist(enumerable);
        //
        //     return CreateSuccessfulImportMessage(numberOfImportedAlbums);
        // }
        //
        private static string CreateSuccessfulImportMessage(int numberOfImportedAlbums)
        {
            return numberOfImportedAlbums > 0 ?
                numberOfImportedAlbums == 1 ?
                    "1 album was imported" :
                    $"{numberOfImportedAlbums} albums were imported" :
                "None albums were imported";
        }
        
        private bool IsFileProvided(IFormFile file)
        {
            return file != null; // && file.ContentLength > 0;
        }
        
        private static bool IsAlbumsRetrievedFromFile(IEnumerable<Album> albums)
        {
            return albums != null && albums.Any();
        }
        
        private static IEnumerable<Album> GetAlbumsWithImageFromFile(
            Stream fileStream,
            IInsecureDeserializer<string, IEnumerable<Album>> fileDeserializer)
        {
            // full path to file in temp location
            var filePath = Path.GetTempFileName();

            using var streamReader = new StreamReader(fileStream);
            return fileDeserializer.Deserialize(streamReader.ReadToEnd());
        }
    }
}