using System.Runtime.Serialization;

namespace GodelTech.Owasp.Web.Models
{
    [DataContract]
    public class AlbumWithImage : Album
    {
        [DataMember]
        public dynamic Image { get; set; }
    }
}