using System;

namespace GodelTech.Owasp.InsecureDeserialization.Interfaces
{
    public interface IInsecureDeserializer<in T, out TR>
    {
        TR Deserialize(T dataToDeserialize);
    }

    public interface IInsecureDeserializer<in T>
    {
        dynamic Deserialize(T dataToDeserialize, Type type);
    }
}
