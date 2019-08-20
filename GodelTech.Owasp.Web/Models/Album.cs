using System.Runtime.Serialization;

namespace GodelTech.Owasp.Web.Models
{
    [DataContract]
    public class Album
    {
        [DataMember]
        public int AlbumId { get; set; }
        [DataMember]
        public int GenreId { get; set; }
        [DataMember]
        public int ArtistId { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public string AlbumArtUrl { get; set; }
    }
}