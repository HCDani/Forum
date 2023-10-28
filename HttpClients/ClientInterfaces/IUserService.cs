using Models.DTO;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

namespace HttpClients.ClientInterfaces
{
    public interface IUserService
    {
        Task<User> Create(UserCreationDto dto);
        Task<IEnumerable<User>> GetUsers(string? usernameContains = null);
        Task LoginAsync(AuthUserDTO dto);
        Task LogoutAsync();

        Task<ClaimsPrincipal> GetAuthAsync();

        Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
    }
}
