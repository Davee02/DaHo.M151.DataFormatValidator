using DaHo.M151.DataFormatValidator.Abstractions;
using DaHo.M151.DataFormatValidator.Models;
using System;

namespace DaHo.M151.DataFormatValidator.Services
{
    public class XmlFormatService : IDataFormatService
    {
        public DataFormat Format => DataFormat.XML;

        public (bool Success, string ErrorMessage, dynamic Converted) Deserialize(string content)
        {
            throw new NotImplementedException();
        }

        public (bool Success, string ErrorMessage, string Converted) Serialize(dynamic content)
        {
            throw new NotImplementedException();
        }

        public (bool Success, string ErrorMessage) Validate(string content)
        {
            throw new NotImplementedException();
        }

        public (bool Success, string ErrorMessage) ValidateWithSchema(string content, string schema)
        {
            throw new NotImplementedException();
        }
    }
}
