using DaHo.Library.AspNetCore.Data.Repositories.EntityFramework;
using DaHo.M151.DataFormatValidator.Abstractions;
using DaHo.M151.DataFormatValidator.Models.StorageModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DaHo.M151.DataFormatValidator.Data.Repositories
{
    public class SchemaRepository : GenericEntityRepository<DataSchema, DataContext>, ISchemaRepository
    {
        public SchemaRepository(DataContext context) : base(context)
        {
        }

        public async Task<DataSchema> GetByNameAsync(string name)
        {
            return await Context
                .DataSchemas
                .FirstOrDefaultAsync(x => string.Equals(x.SchemaName, name));
        }
    }
}
