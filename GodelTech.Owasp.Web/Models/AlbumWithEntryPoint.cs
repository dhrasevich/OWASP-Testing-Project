using System.Runtime.Serialization;

namespace GodelTech.Owasp.Web.Models
{
    [DataContract]
    public class AlbumWithEntryPoint : Album
    {
        [DataMember]
        public dynamic EntryPoint { get; set; }
    }
}