using DaHo.Library.AspNetCore.Data.Repositories.Abstractions;
using DaHo.M151.DataFormatValidator.Models.StorageModels;
using System.Threading.Tasks;

namespace DaHo.M151.DataFormatValidator.Abstractions
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByUsernameAndPasswordAsync(string username, string password);
    }
}
