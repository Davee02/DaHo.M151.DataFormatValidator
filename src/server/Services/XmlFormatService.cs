using DaHo.M151.DataFormatValidator.Abstractions;
using DaHo.M151.DataFormatValidator.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace DaHo.M151.DataFormatValidator.Services
{
    public class XmlFormatService : IDataFormatService
    {
        public DataFormat Format => DataFormat.XML;

        public (bool Success, string ErrorMessage, dynamic Converted) Deserialize(string content)
        {
            var validationResult = Validate(content);
            if (!validationResult.Success)
            {
                return (false, validationResult.ErrorMessage, null);
            }

            var doc = XDocument.Parse(content);
            string jsonText = JsonConvert.SerializeXNode(doc);
            var deserialized = JsonConvert.DeserializeObject<dynamic>(jsonText);

            return (true, null, deserialized);
        }

        public (bool Success, string ErrorMessage, string Converted) Serialize(dynamic content)
        {
            var json = JsonConvert.SerializeObject(content);
            var node = JsonConvert.DeserializeXNode(json, "root");
            var serialized = node.ToString();

            return (true, null, serialized);
        }

        public (bool Success, string ErrorMessage) Validate(string content)
        {
            try
            {
                XElement.Parse(content);
            }
            catch (XmlException e)
            {
                return (false, e.Message);
            }

            return (true, null);
        }

        public (bool Success, string ErrorMessage) ValidateWithSchema(string content, string schema)
        {
            var contentValidationResult = Validate(content);
            if (!contentValidationResult.Success)
            {
                return (false, $"The content has an invalid format{Environment.NewLine}{contentValidationResult.ErrorMessage}");
            }

            var schemaValidationResult = Validate(schema);
            if (!schemaValidationResult.Success)
            {
                return (false, $"The schema has an invalid format{Environment.NewLine}{schemaValidationResult.ErrorMessage}");
            }

            var schemas = new XmlSchemaSet();
            schemas.Add(string.Empty, XmlReader.Create(new StringReader(schema)));

            var doc = XDocument.Parse(content);

            var errors = string.Empty;
            doc.Validate(schemas, (o, e) =>
            {
                if (e.Severity == XmlSeverityType.Error)
                {
                    errors += $"{e.Message}{Environment.NewLine}";
                }
            });

            return string.IsNullOrWhiteSpace(errors)
                ? (true, null)
                : (false, errors);
        }
    }
}
