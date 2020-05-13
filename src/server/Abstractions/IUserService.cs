using DaHo.M151.DataFormatValidator.Models.ServiceModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DaHo.M151.DataFormatValidator.Abstractions
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);

        Task<IEnumerable<User>> GetAll();

        Task<User> GetById(int id);
    }
}
