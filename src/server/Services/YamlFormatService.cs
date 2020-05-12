using DaHo.M151.DataFormatValidator.Abstractions;
using DaHo.M151.DataFormatValidator.Models;
using System;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace DaHo.M151.DataFormatValidator.Services
{
    public class YamlFormatService : IDataFormatService
    {
        public DataFormat Format => DataFormat.YAML;

        public (bool Success, string ErrorMessage, dynamic Converted) Deserialize(string content)
        {
            var validationResult = Validate(content);
            if (!validationResult.Success)
            {
                return (false, validationResult.ErrorMessage, null);
            }

            var deserializer = new Deserializer();
            var deserialized = deserializer.Deserialize<dynamic>(content);

            return (true, null, deserialized);
        }

        public (bool Success, string ErrorMessage, string Converted) Serialize(dynamic content)
        {
            var serializer = new Serializer();
            var serialized = serializer.Serialize(content);

            return (true, null, serialized);
        }

        public (bool Success, string ErrorMessage) Validate(string content)
        {
            try
            {
                var deserializer = new Deserializer();
                deserializer.Deserialize<dynamic>(content);
            }
            catch (YamlException e)
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

            // For YAML, no schema exists. So we always return sucess
            return (true, null);
        }
    }
}
