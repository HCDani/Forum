using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfcDataAccess.DAOs
{
    public class PostEfcDao : IPostDao
    {
        private readonly PostContext context;
        public async Task<Post> CreateAsync(Post post)
        {
            EntityEntry<Post> added = await context.Posts.AddAsync(post);
            await context.SaveChangesAsync();
            return added.Entity;
        }

        public async Task DeleteAsync(int id)
        {
            Post? existing = await GetByIdAsync(id);
            if (existing == null)
            {
                throw new Exception($"Post with id {id} not found");
            }

            context.Posts.Remove(existing);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
        {
            IQueryable<Post> query = context.Posts.Include(post => post.Owner).AsQueryable();

            if (!string.IsNullOrEmpty(searchParameters.Username))
            {
                // we know username is unique, so just fetch the first
                query = query.Where(todo =>
                    todo.Owner.UserName.ToLower().Equals(searchParameters.Username.ToLower()));
            }

            if (searchParameters.UserId != null)
            {
                query = query.Where(t => t.Owner.Id == searchParameters.UserId);
            }
            if (!string.IsNullOrEmpty(searchParameters.TitleContains))
            {
                query = query.Where(t =>
                    t.Title.ToLower().Contains(searchParameters.TitleContains.ToLower()));
            }

            List<Post> result = await query.ToListAsync();
            return result;
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            Post? found = await context.Posts
            .AsNoTracking()
            .Include(post => post.Owner)
            .SingleOrDefaultAsync(post => post.Id == id);
            return found;
        }

        public async Task UpdateAsync(Post post)
        {
            context.Posts.Update(post);
            await context.SaveChangesAsync();
        }
    }
}
