using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DaoInterfaces
{
    public interface IPostDao
    {
        Task<Post> CreateAsync(Post post);
        Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters);
        Task UpdateAsync(Post post);
        Task<Post> GetByIdAsync(int id);
        Task DeleteAsync(int id);
    }
}
