using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using GodelTech.Owasp.InsecureDeserialization.Interfaces;

namespace GodelTech.Owasp.InsecureDeserialization.Implementations
{
    public class InsecureBinaryDeserializer<TR> : IInsecureDeserializer<string, TR> where TR : class
    {
        public TR Deserialize(string base64EncodedData)
        {
            if (base64EncodedData == null)
            {
                throw new ArgumentNullException(nameof(base64EncodedData));
            }

            var binaryFormatter = new BinaryFormatter();

            using (var memoryStream = new MemoryStream(Convert.FromBase64String(base64EncodedData)))
            {
                return binaryFormatter.Deserialize(memoryStream) as TR;
            }
        }
    }
}