using System.IO;

namespace FluentlyExport
{
    public class DefaultXmlSerializer<T> : ISerializer<T>
    {
        public byte[] Serialize(T t)
        {
            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof (T));
            using (var stream = new MemoryStream())
            {
                xmlSerializer.Serialize(stream, t);
                stream.Position = 0;
                return stream.ToArray();
            }
        }
    }
}