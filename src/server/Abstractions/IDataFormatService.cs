using DaHo.M151.DataFormatValidator.Models;
using System.Collections.Generic;

namespace DaHo.M151.DataFormatValidator.Abstractions
{
    public interface IDataFormatService
    {
        (bool Success, string ErrorMessage) Validate(string content);

        (bool Success, string ErrorMessage) ValidateWithSchema(string content, string schema);

        (bool Success, string ErrorMessage, dynamic Converted) Deserialize(string content);

        (bool Success, string ErrorMessage, string Converted) Serialize(dynamic content);

        DataFormat Format { get; }
    }
}
