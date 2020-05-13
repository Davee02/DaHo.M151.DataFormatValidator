using DaHo.Library.AspNetCore.Data.Repositories.EntityFramework;
using DaHo.M151.DataFormatValidator.Abstractions;
using DaHo.M151.DataFormatValidator.Models.StorageModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DaHo.M151.DataFormatValidator.Data.Repositories
{
    public class UserRepository : GenericEntityRepository<User, DataContext>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {
        }

        public Task<User> GetByUsernameAndPasswordAsync(string username, string password)
        {
            return Context
                .Users
                .SingleOrDefaultAsync(x => string.Equals(x.Username, username) && string.Equals(x.Password, password));
        }
    }
}
