using DaHo.M151.DataFormatValidator.Abstractions;
using DaHo.M151.DataFormatValidator.Models;
using DaHo.M151.DataFormatValidator.Models.ApiModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaHo.Library.Utilities;

namespace DaHo.M151.DataFormatValidator.Controllers
{
    [Authorize]
    [Produces("application/json")]
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

        /// <summary>
        /// Converts data from one format to another
        /// </summary>
        /// <param name="request">The data required to perform the convertion</param>
        [HttpPost("convert")]
        [ProducesResponseType(typeof(ConvertFormatResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult ConvertFormat([FromBody] ConvertFormatRequest request)
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

        /// <summary>
        /// Validates whether data is in a valid state for a given dataformat
        /// </summary>
        /// <param name="request">The data required to perform the validation</param>
        [HttpPost("validate")]
        [ProducesResponseType(typeof(ValidateFormatResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult ValidateFormat([FromBody] ValidateFormatRequest request)
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

        /// <summary>
        /// Validates whether data is in a valid state for a given dataformat and a schema
        /// </summary>
        /// <param name="request">The data required to perform the validation</param>
        /// <param name="schemaName">The name of the schema that is used for the validation</param>
        [HttpPost("validate/{schemaName}")]
        [ProducesResponseType(typeof(ValidateFormatResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ValidateFormatWithSchema([FromBody] ValidateFormatRequest request, [FromRoute] string schemaName)
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

        /// <summary>
        /// Gets all available dataformats
        /// </summary>
        [HttpGet("list")]
        [ProducesResponseType(typeof(IEnumerable<DataFormatResponse>), StatusCodes.Status200OK)]
        public IActionResult GetAllDataFormats()
        {
            var allFormats = EnumUtilities
                .GetEnumValues<DataFormat>()
                .Select(x => new DataFormatResponse
                {
                    Key = x.ToString().ToLower(),
                    Value = x.ToString().ToUpper()
                });

            return Ok(allFormats);
        }
    }
}
