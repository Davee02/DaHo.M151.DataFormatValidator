using DaHo.M151.DataFormatValidator.Abstractions;
using DaHo.M151.DataFormatValidator.Models.ApiModels;
using DaHo.M151.DataFormatValidator.Models.ServiceModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaHo.M151.DataFormatValidator.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class SchemasController : ControllerBase
    {
        private readonly ISchemaRepository _schemaRepository;

        public SchemasController(ISchemaRepository schemaRepository)
        {
            _schemaRepository = schemaRepository;
        }

        /// <summary>
        /// Gets all saved schemas
        /// </summary>
        /// <returns>All saved schemas</returns>
        /// <response code="200">Returns all schemas</response>
        [HttpGet("")]
        [ProducesResponseType(typeof(IEnumerable<DataSchema>), StatusCodes.Status200OK)]
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

        /// <summary>
        /// Gets a schema by its name
        /// </summary>
        /// <param name="schemaName">The name of the schema to search</param>
        /// <returns>The found schema</returns>
        /// <response code="200">Returns the found schema</response>
        /// <response code="404">If no schema with this name exists</response>
        [HttpGet("{schemaName}")]
        [ProducesResponseType(typeof(DataSchema), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Creates a new schema
        /// </summary>
        /// <param name="schema">The schema to create</param>
        /// <response code="200">If the schema was created succesfully</response>
        /// <response code="400">If a schema with the same name already exists</response>
        [Authorize(Roles = Role.Admin)]
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Edits an existing schema
        /// </summary>
        /// <param name="schema">The new values of the schema</param>
        /// <param name="schemaName">The name of the schema to edit</param>
        /// <response code="200">If the schema was edited succesfully</response>
        /// <response code="400">If the name of the schema doesn't match</response>
        /// <response code="404">If no schema with the name exists. In this case, create a new schema</response>
        [Authorize(Roles = Role.Admin)]
        [HttpPost("{schemaName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Deletes a schema
        /// </summary>
        /// <param name="schemaName">The name of the schema to delete</param>
        /// <response code="200">If the schema was deleted succesfully</response>
        /// <response code="404">If no schema with the name exists</response>
        [Authorize(Roles = Role.Admin)]
        [HttpDelete("{schemaName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
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
