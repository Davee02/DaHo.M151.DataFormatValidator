using DaHo.Library.AspNetCore.Data.Repositories.Abstractions;
using DaHo.M151.DataFormatValidator.Models.StorageModels;
using System.Threading.Tasks;

namespace DaHo.M151.DataFormatValidator.Abstractions
{
    public interface ISchemaRepository : IGenericRepository<DataSchema>
    {
        Task<DataSchema> GetByNameAsync(string name);
    }
}
