using DaHo.M151.DataFormatValidator.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;

namespace DaHo.M151.DataFormatValidator.Abstractions.Services
{
    public class JsonFormatService : IDataFormatService
    {
        public DataFormat Format => DataFormat.JSON;

        public (bool Success, string ErrorMessage, string Converted) Serialize(dynamic content)
        {
            var serialized = JsonConvert.SerializeObject(content);

            return (true, null, serialized);
        }

        public (bool Success, string ErrorMessage, dynamic Converted) Deserialize(string content)
        {
            var validationResult = Validate(content);
            if (!validationResult.Success)
            {
                return (false, validationResult.ErrorMessage, null);
            }

            var deserialized = JsonConvert.DeserializeObject<dynamic>(content);

            return (true, null, deserialized);
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

            var jsonSchema = JSchema.Parse(schema);
            var jsonContent = JToken.Parse(content);

            if (jsonContent.IsValid(jsonSchema, out IList<string> errors))
            {
                return (true, null);
            }
            else
            {
                return (false, string.Join(Environment.NewLine, errors));
            }
        }

        public (bool Success, string ErrorMessage) Validate(string content)
        {
            try
            {
                JToken.Parse(content);
            }
            catch (JsonReaderException ex)
            {
                return (false, ex.Message);
            }

            return (true, null);
        }
    }
}
