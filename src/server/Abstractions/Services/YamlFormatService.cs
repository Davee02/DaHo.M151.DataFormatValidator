using DaHo.M151.DataFormatValidator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaHo.M151.DataFormatValidator.Abstractions.Services
{
    public class YamlFormatService : IDataFormatService
    {
        public DataFormat Format => DataFormat.YAML;

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
