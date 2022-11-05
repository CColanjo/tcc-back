using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace schedule_appointment_domain.Helpers;

public class XmlSerialization
    {
        public static string Serialize<T>(T value, Encoding encoding, bool indent = true, bool omitXmlDeclaration = false, bool omitNamespacesDeclaration = true, XmlSerializerNamespaces xmlSerializerNamespaces = null)
        {
            if (value == null)
            {
                return null;
            }

            var serializer = new XmlSerializer(typeof(T));

            var settings = new XmlWriterSettings();

            settings.Encoding = encoding;
            settings.Indent = indent;
            settings.OmitXmlDeclaration = omitXmlDeclaration;

            string serializedString = null;

            using (var textWriter = new StringWriterWithEncoding(encoding))
            {
                using (var xmlWriter = XmlWriter.Create(textWriter, settings))
                {
                    if (xmlSerializerNamespaces == null && omitNamespacesDeclaration)
                    {
                        xmlSerializerNamespaces = new XmlSerializerNamespaces();

                        xmlSerializerNamespaces.Add("", "");
                    }

                    serializer.Serialize(xmlWriter, value, xmlSerializerNamespaces);
                }

                serializedString = textWriter.ToString();
            }

            return serializedString;
        }

        public static T Deserialize<T>(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return default(T);
            }

            var serializer = new XmlSerializer(typeof(T));

            using (var textReader = new StringReader(xml))
            {
                var settings = new XmlReaderSettings();

                using (var xmlReader = XmlReader.Create(textReader, settings))
                {
                    return (T)serializer.Deserialize(xmlReader);
                }
            }
        }

        public static T DeserializeWithNamespaces<T>(string xml)
        {
            var pageDeserializer = new XmlSerializer(typeof(T));

            using (var txReader = new StringReader(xml))
            {
                // Create XmlReaderSettings
                var settings = new XmlReaderSettings();

                settings.ConformanceLevel = ConformanceLevel.Fragment;
                settings.IgnoreWhitespace = true;
                settings.IgnoreComments = true;

                // Create a new NameTable
                var nt = new NameTable();

                // Create a new NamespaceManager
                var nsmgr = new XmlNamespaceManager(nt);

                // Add your namespaces used in the XML
                nsmgr.AddNamespace("", "http://www.cip-bancos.org.br/ARQ/ASLC027.xsd");

                // Create the XmlParserContext using the previous declared XmlNamespaceManager
                var ctx = new XmlParserContext(null, nsmgr, null, XmlSpace.None);

                // Instantiate a new XmlReader, using the previous declared XmlReaderSettings and XmlParserContext
                using (var reader = XmlReader.Create(txReader, settings, ctx))
                {
                    // Finally, deserialize
                    T deserialized = (T)pageDeserializer.Deserialize(reader);

                    return deserialized;
                }
            }
        }
    }