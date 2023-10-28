using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients.ClientInterfaces
{
    public interface IPostService
    {
        Task CreateAsync(PostCreationDto dto);
        Task<ICollection<Post>> GetAsync(
        string? userName,
        int? userId,
        string? titleContains
    );
        Task UpdateAsync(PostUpdateDto dto);
        Task<PostBasicDto> GetByIdAsync(int id);
        Task DeleteAsync(int id);
    }

}
