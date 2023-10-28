using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.LogicInterfaces
{
    public interface IUserLogic
    {
        public Task<User> CreateAsync(UserCreationDto dto);
        public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters);
        public Task<User>ValidateUser(string userName, string password);
    }
}
