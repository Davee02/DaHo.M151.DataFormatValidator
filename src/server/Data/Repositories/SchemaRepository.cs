using DaHo.Library.AspNetCore.Data.Repositories.EntityFramework;
using DaHo.M151.DataFormatValidator.Abstractions;
using DaHo.M151.DataFormatValidator.Data.Repositories.Extensions;
using DaHo.M151.DataFormatValidator.Models.StorageModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DaHo.M151.DataFormatValidator.Data.Repositories
{
    public class SchemaRepository : GenericEntityRepository<DataSchema, DataContext>, ISchemaRepository
    {
        private readonly IDistributedCache _cache;
        private readonly Func<string, string> _cacheKeyGenerator = (schemaName) => $"Schema_{schemaName}";
        private const string ALL_SCHEMAS_CACHE_KEY = "AllSchemas";

        public SchemaRepository(
            DataContext context,
            IDistributedCache cache) : base(context)
        {
            _cache = cache;
        }

        public async Task<DataSchema> GetByNameAsync(string name)
        {
            var schema = await _cache.GetObjectAsync<DataSchema>(_cacheKeyGenerator(name));

            if (schema == null)
            {
                schema = await Context
                            .DataSchemas
                            .FirstOrDefaultAsync(x => string.Equals(x.SchemaName, name));

                await _cache.SetObjectAsync(_cacheKeyGenerator(name), schema);
            }

            return schema;
        }

        public override async Task<IEnumerable<DataSchema>> GetAllAsync()
        {
            var schemas = await _cache.GetObjectAsync<IEnumerable<DataSchema>>("AllSchemas");

            if (schemas == null)
            {
                schemas = await base.GetAllAsync();
                await _cache.SetObjectAsync(ALL_SCHEMAS_CACHE_KEY, schemas);
            }

            return schemas;
        }

        public override async Task DeleteAsync(DataSchema entity)
        {
            await _cache.RemoveAsync(_cacheKeyGenerator(entity.SchemaName));

            await base.DeleteAsync(entity);
        }
    }
}
