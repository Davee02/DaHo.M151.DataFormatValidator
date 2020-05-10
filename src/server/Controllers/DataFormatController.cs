using DaHo.M151.DataFormatValidator.Abstractions;
using DaHo.M151.DataFormatValidator.Models.ApiModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace DaHo.M151.DataFormatValidator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataFormatController : ControllerBase
    {
        private readonly IEnumerable<IDataFormatService> _dataFormatServices;

        public DataFormatController(IEnumerable<IDataFormatService> dataFormatServices)
        {
            _dataFormatServices = dataFormatServices;
        }

        [Route("convert")]
        [HttpPost]
        public IActionResult ConvertFormat(ConvertFormatRequest request)
        {
            var sourceDataFormatService = _dataFormatServices.FirstOrDefault(x => x.Format == request.From);
            var targetDataFormatService = _dataFormatServices.FirstOrDefault(x => x.Format == request.To);

            if (sourceDataFormatService == null)
            {
                return BadRequest("The source dataformat is not supported");
            }

            if (targetDataFormatService == null)
            {
                return BadRequest("The source dataformat is not supported");
            }

            var deserializeResult = sourceDataFormatService.Deserialize(request.Content);
            if (!deserializeResult.Success)
            {
                return BadRequest($"The source-content is not valid: ${deserializeResult.ErrorMessage}");
            }

            var serializeContent = targetDataFormatService.Serialize(deserializeResult.Converted);
            if (!serializeContent.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"The content could not be converted to the specified dataformat: ${serializeContent.ErrorMessage}");
            }

            var response = new ConvertFormatResponse { Converted = serializeContent.Converted };

            return Ok(response);
        }

        [Route("validate")]
        [HttpPost]
        public IActionResult ValidateFormat(ValidateFormatRequest request)
        {
            var dataFormatService = _dataFormatServices.FirstOrDefault(x => x.Format == request.Format);

            if (dataFormatService == null)
            {
                return BadRequest("The dataformat is not supported");
            }

            var validationResult = dataFormatService.Validate(request.Content);
            var response = new ValidateFormatResponse { IsValid = validationResult.Success, ErrorMessage = validationResult.ErrorMessage };

            return Ok(response);
        }
    }
}
