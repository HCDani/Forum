using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DaoInterfaces
{
    public interface IUserDao
    {
        Task<User> CreateAsync(User user);
        Task<User?> GetByUsernameAsync(string userName);
        public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters);
        Task<User?> GetByIdAsync(int id);
        
    }
}
