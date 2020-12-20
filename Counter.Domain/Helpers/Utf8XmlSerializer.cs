using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Counter.Domain.Helpers
{
    public class Utf8XmlSerializer : IXmlSerializer
    {
        private static XmlWriterSettings _settings = new XmlWriterSettings()
        {
            OmitXmlDeclaration = true,
        };

        private readonly XmlSerializerNamespaces _emptyNamespace;

        public Utf8XmlSerializer()
        {
            _emptyNamespace = new XmlSerializerNamespaces();
            _emptyNamespace.Add("", "");
    }

        public string Serialize<T>(T data, Type type)
        {
            var settings = new XmlWriterSettings()
            {
                OmitXmlDeclaration = true,
            };

            var serializer = new XmlSerializer(type);

            using (var stringWriter = new Utf8StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(stringWriter, settings))
                {
                    serializer.Serialize(xmlWriter, data, _emptyNamespace);
                }

                var xml = stringWriter.ToString();
                return xml;
            }
        }

        public sealed class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding => Encoding.UTF8;
        }
    }
}
