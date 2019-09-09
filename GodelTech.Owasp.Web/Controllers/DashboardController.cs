using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using GodelTech.Owasp.Web.Repositories;
using System.Web.Mvc;
using GodelTech.Owasp.InsecureDeserialization.Implementations;
using GodelTech.Owasp.InsecureDeserialization.Interfaces;
using GodelTech.Owasp.Web.Models;

namespace GodelTech.Owasp.Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        AlbumRepository repository;
        private IInsecureDeserializer<string, IEnumerable<Album>> binaryFormatterDeserializer;
        private IInsecureDeserializer<string, IEnumerable<AlbumWithEntryPoint>> newtonSoftJsonDeserializer;

        public DashboardController()
        {
            repository = new AlbumRepository();
            binaryFormatterDeserializer = new InsecureBinaryDeserializer<IEnumerable<Album>>();
            newtonSoftJsonDeserializer = new InsecureNewtonSoftJsonDeserializer<IEnumerable<AlbumWithEntryPoint>>();
        }

        public ActionResult Import()
        {
            var model = new AlbumRepository().GetList(0, int.MaxValue);
            return View(model);
        }

        [HttpPost]
        public ActionResult ImportFromBinaryFile(HttpPostedFileBase file)
        {
            try
            {
                ViewBag.Message = ImportAlbums(file, binaryFormatterDeserializer);
                return View("Index");
            }
            catch
            {
                ViewBag.Message = "Albums import failed";
                return View("Index");
            }
        }

        [HttpPost]
        public ActionResult ImportFromJsonFile(HttpPostedFileBase file)
        {
            try
            {
                ViewBag.Message = ImportAlbums(file, newtonSoftJsonDeserializer);
                return View("Index");
            }
            catch
            {
                ViewBag.Message = "Albums import failed";
                return View("Index");
            }
        }

        private string ImportAlbums(
            HttpPostedFileBase file,
            IInsecureDeserializer<string, IEnumerable<Album>> fileDeserializer)
        {
            if (!IsFileProvided(file))
            {
                return "Albums were not imported. File is not provided or empty";
            }

            var albums = GetAlbumsWithImageFromFile(file.InputStream, fileDeserializer);

            var enumerable = albums.ToList();

            if (!IsAlbumsRetrievedFromFile(enumerable))
            {
                return "Albums were not imported. File does not contain albums";
            }

            var numberOfImportedAlbums = repository.AddIfNotExist(enumerable);

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

        private bool IsAlbumsRetrievedFromFile(IEnumerable<Album> albums)
        {
            return albums != null && albums.Any();
        }

        private IEnumerable<Album> GetAlbumsWithImageFromFile(
            Stream fileStream,
            IInsecureDeserializer<string, IEnumerable<Album>> fileDeserializer)
        {
            using (var streamReader = new StreamReader(fileStream))
            {
                return fileDeserializer.Deserialize(streamReader.ReadToEnd());
            }
        }
    }
}