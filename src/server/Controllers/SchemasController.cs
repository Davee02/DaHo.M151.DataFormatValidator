using DaHo.M151.DataFormatValidator.Abstractions;
using DaHo.M151.DataFormatValidator.Models.ApiModels;
using DaHo.M151.DataFormatValidator.Models.ServiceModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace DaHo.M151.DataFormatValidator.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SchemasController : ControllerBase
    {
        private readonly ISchemaRepository _schemaRepository;

        public SchemasController(ISchemaRepository schemaRepository)
        {
            _schemaRepository = schemaRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllSchemas()
        {
            var schemas = await _schemaRepository.GetAllAsync();
            var response = schemas.Select(x => new DataSchema
            {
                ForFormat = x.ForFormat,
                Schema = x.Schema,
                Name = x.SchemaName
            });

            return Ok(response);
        }

        [HttpGet("{schemaName}")]
        public async Task<IActionResult> GetSchema([FromRoute] string schemaName)
        {
            var foundSchema = await _schemaRepository.GetByNameAsync(schemaName);

            if (foundSchema == null)
            {
                return NotFound($"The schema with the name '{schemaName}' does not exist");
            }

            var response = new DataSchema
            {
                ForFormat = foundSchema.ForFormat,
                Schema = foundSchema.Schema,
                Name = foundSchema.SchemaName
            };

            return Ok(response);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("")]
        public async Task<IActionResult> CreateSchema([FromBody] DataSchema schema)
        {
            var foundSchema = await _schemaRepository.GetByNameAsync(schema.Name);

            if(foundSchema != null)
            {
                return BadRequest($"A schema with the name '{schema.Name}' already exists");
            }

            var storageModel = new Models.StorageModels.DataSchema
            {
                ForFormat = schema.ForFormat,
                Schema = schema.Schema,
                SchemaName = schema.Name
            };
            await _schemaRepository.CreateAsync(storageModel);

            return Ok();
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("{schemaName}")]
        public async Task<IActionResult> EditSchema([FromBody] DataSchema schema, [FromRoute] string schemaName)
        {
            if(!string.Equals(schema.Name, schemaName))
            {
                return BadRequest("The schema name cannot be updated");
            }

            var foundSchema = await _schemaRepository.GetByNameAsync(schemaName);

            if (foundSchema == null)
            {
                return NotFound($"The schema with the name '{schemaName}' does not exist");
            }

            foundSchema.ForFormat = schema.ForFormat;
            foundSchema.Schema = schema.Schema;

            await _schemaRepository.UpdateAsync(foundSchema);

            return Ok();
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("{schemaName}")]
        public async Task<IActionResult> DeleteSchema([FromRoute] string schemaName)
        {
            var foundSchema = await _schemaRepository.GetByNameAsync(schemaName);

            if(foundSchema == null)
            {
                return NotFound($"The schema with the name '{schemaName}' does not exist");
            }

            await _schemaRepository.DeleteAsync(foundSchema);

            return Ok();
        }
    }
}
