using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.LogicInterfaces
{
    public interface IPostLogic
    {
        Task<Post> CreateAsync(PostCreationDto dto);
        Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters);
        Task UpdateAsync(PostUpdateDto post);
        Task DeleteAsync(int id);
        Task<PostBasicDto> GetByIdAsync(int id);
    }
}
