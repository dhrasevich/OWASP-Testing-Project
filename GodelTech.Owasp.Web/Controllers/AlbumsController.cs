using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using GodelTech.Owasp.Web.Repositories;
using System.Web.Mvc;
using GodelTech.Owasp.InsecureDeserialization.Interfaces;
using GodelTech.Owasp.Web.Models;

namespace GodelTech.Owasp.Web.Controllers
{
    public class AlbumsController : Controller
    {
        AlbumRepository repository;
        private IInsecureDeserializer<string, IEnumerable<AlbumWithImage>> binaryFormatterDeserializer;
        private IInsecureDeserializer<string, IEnumerable<AlbumWithImage>> newtonJsonDeserializer;

        public AlbumsController()
        {
            repository = new AlbumRepository();
        }

        public ActionResult Details(string id)
        {
            var repository = new AlbumRepository();
            var model = repository.Get(id);
            return View(model);
        }

        public ActionResult List(string searchKey)
        {
            var model = repository.GetList(searchKey);
            return View(model);
        }

        public ActionResult ImportFromBinaryFile(HttpPostedFileBase file)
        {
            try
            {
                ViewBag.Message = ImportAlbums(file, binaryFormatterDeserializer);
                return View();
            }
            catch
            {
                ViewBag.Message = "Albums import failed";
                return View();
            }
        }

        public ActionResult ImportFromJsonFile(HttpPostedFileBase file)
        {
            try
            {
                ViewBag.Message = ImportAlbums(file, newtonJsonDeserializer);
                return View();
            }
            catch
            {
                ViewBag.Message = "Albums import failed";
                return View();
            }
        }

        private string ImportAlbums(
            HttpPostedFileBase file,
            IInsecureDeserializer<string, IEnumerable<AlbumWithImage>> fileDeserializer)
        {
            if (!IsFileProvided(file))
            {
                return "Albums were not imported. File is not provided or empty";
            }

            var albums = GetAlbumsWithImageFromFile(file.InputStream, newtonJsonDeserializer);

            if (!IsAlbumsRetrievedFromFile(albums))
            {
                return "Albums were not imported. File does not contain albums";
            }

            var numberOfImportedAlbums = repository.AddIfNotExist(albums);

            return CreateSuccessfulImportMessage(numberOfImportedAlbums);
        }

        private string CreateSuccessfulImportMessage(int numberOfImportedAlbums)
        {
            return numberOfImportedAlbums > 0 ?
                numberOfImportedAlbums == 1 ?
                    "1 album was imported" :
                    $"{numberOfImportedAlbums} albums were imported" :
                "None albums were imported";
        }

        private bool IsFileProvided(HttpPostedFileBase file)
        {
            return file != null && file.ContentLength > 0;
        }

        private bool IsAlbumsRetrievedFromFile(IEnumerable<AlbumWithImage> albums)
        {
            return albums != null && albums.Any();
        }

        private IEnumerable<AlbumWithImage> GetAlbumsWithImageFromFile(
            Stream fileStream,
            IInsecureDeserializer<string, IEnumerable<AlbumWithImage>> fileDeserializer)
        {
            using (var streamReader = new StreamReader(fileStream))
            {
                return fileDeserializer.Deserialize(streamReader.ReadToEnd());
            }
        }
    }
}