﻿using Application.DaoInterfaces;
using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileData.DAOs
{
    public class PostFileDao : IPostDao
    {
        private readonly FileContext context;

        public PostFileDao(FileContext context)
        {
            this.context = context;
        }
        public Task<Post> CreateAsync(Post post)
        {
            int id = 1;
            if (context.Posts.Any())
            {
                id = context.Posts.Max(t => t.Id);
                id++;
            }

            post.Id = id;

            context.Posts.Add(post);
            context.SaveChanges();

            return Task.FromResult(post);
        }
        public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParams)
        {
            IEnumerable<Post> result = context.Posts.AsEnumerable();

            if (!string.IsNullOrEmpty(searchParams.Username))
            {
                // we know username is unique, so just fetch the first
                result = context.Posts.Where(post =>
                    post.Owner.UserName.Equals(searchParams.Username, StringComparison.OrdinalIgnoreCase));
            }

            if (searchParams.UserId != null)
            {
                result = result.Where(p => p.Owner.Id == searchParams.UserId);
            }
            if (!string.IsNullOrEmpty(searchParams.TitleContains))
            {
                result = result.Where(p =>
                    p.Title.Contains(searchParams.TitleContains, StringComparison.OrdinalIgnoreCase));
            }

            return Task.FromResult(result);
        }
        public Task<Post?> GetByIdAsync(int postId)
        {
            Post? existing = context.Posts.FirstOrDefault(p => p.Id == postId);
            return Task.FromResult(existing);
        }
        public Task UpdateAsync(Post toUpdate)
        {
            Post? existing = context.Posts.FirstOrDefault(post => post.Id == toUpdate.Id);
            if (existing == null)
            {
                throw new Exception($"Post with id {toUpdate.Id} does not exist!");
            }

            context.Posts.Remove(existing);
            context.Posts.Add(toUpdate);

            context.SaveChanges();

            return Task.CompletedTask;
        }
        public Task DeleteAsync(int id)
        {
            Post? existing = context.Posts.FirstOrDefault(post => post.Id == id);
            if (existing == null)
            {
                throw new Exception($"Post with id {id} does not exist!");
            }

            context.Posts.Remove(existing);
            context.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
