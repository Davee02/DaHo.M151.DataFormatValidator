using DaHo.M151.DataFormatValidator.Abstractions;
using DaHo.M151.DataFormatValidator.Models.ApiModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaHo.M151.DataFormatValidator.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class DataFormatController : ControllerBase
    {
        private readonly IEnumerable<IDataFormatService> _dataFormatServices;
        private readonly ISchemaRepository _schemaRepository;

        public DataFormatController(
            IEnumerable<IDataFormatService> dataFormatServices, 
            ISchemaRepository schemaRepository)
        {
            _dataFormatServices = dataFormatServices;
            _schemaRepository = schemaRepository;
        }

        [HttpPost("convert")]
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

            (bool Success, string ErrorMessage, dynamic Converted) deserializeResult = sourceDataFormatService.Deserialize(request.Content);
            if (!deserializeResult.Success)
            {
                return BadRequest($"The source-content is not valid: ${deserializeResult.ErrorMessage}");
            }

            (bool Success, string ErrorMessage, string Converted) serializeContent = targetDataFormatService.Serialize(deserializeResult.Converted);
            if (!serializeContent.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"The content could not be converted to the specified dataformat: ${serializeContent.ErrorMessage}");
            }

            var response = new ConvertFormatResponse { Converted = serializeContent.Converted };

            return Ok(response);
        }

        [HttpPost("validate")]
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

        [HttpPost("validate/{schemaName}")]
        public async Task<IActionResult> ValidateFormatWithSchema(ValidateFormatRequest request, string schemaName)
        {
            var dataFormatService = _dataFormatServices.FirstOrDefault(x => x.Format == request.Format);

            if (dataFormatService == null)
            {
                return BadRequest("The dataformat is not supported");
            }

            var schema = await _schemaRepository.GetByNameAsync(schemaName);
            if (schema == null)
            {
                return NotFound($"The schema with the name '{schemaName}' does not exist");
            }

            var validationResult = dataFormatService.ValidateWithSchema(request.Content, schema.Schema);
            var response = new ValidateFormatResponse { IsValid = validationResult.Success, ErrorMessage = validationResult.ErrorMessage };

            return Ok(response);
        }
    }
}
