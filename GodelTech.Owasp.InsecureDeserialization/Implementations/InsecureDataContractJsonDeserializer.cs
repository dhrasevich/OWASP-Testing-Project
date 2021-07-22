using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using GodelTech.Owasp.InsecureDeserialization.Interfaces;

namespace GodelTech.Owasp.InsecureDeserialization.Implementations
{
    public class InsecureDataContractJsonDeserializer : IInsecureDeserializer<string>
    {
        public dynamic Deserialize(string dataToDeserialize, Type type)
        {
            if (dataToDeserialize == null)
            {
                throw new ArgumentNullException(nameof(dataToDeserialize));
            }

            var dataContractJsonSerializer = new DataContractJsonSerializer(type);

            using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(dataToDeserialize)))
            {
                return dataContractJsonSerializer.ReadObject(memoryStream);
            }
        }
    }
}