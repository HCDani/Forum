﻿using HttpClients.ClientInterfaces;
using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HttpClients.Implementations
{
    public class PostHttpClient : IPostService
    {
        private readonly HttpClient client;

        public PostHttpClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task CreateAsync(PostCreationDto dto)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("/post", dto);
            if (!response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                throw new Exception(content);
            }
        }
        public async Task<ICollection<Post>> GetAsync(string? userName, int? userId, string? titleContains)
        {
            string query = ConstructQuery(userName, userId, titleContains);
            HttpResponseMessage response = await client.GetAsync("/post"+query);
            string content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }

            ICollection<Post> posts = JsonSerializer.Deserialize<ICollection<Post>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
            return posts;
        }
        private static string ConstructQuery(string? userName, int? userId, string? titleContains)
        {
            string query = "";
            if (!string.IsNullOrEmpty(userName))
            {
                query += $"?username={userName}";
            }

            if (userId != null)
            {
                query += string.IsNullOrEmpty(query) ? "?" : "&";
                query += $"userid={userId}";
            }
            if (!string.IsNullOrEmpty(titleContains))
            {
                query += string.IsNullOrEmpty(query) ? "?" : "&";
                query += $"titlecontains={titleContains}";
            }

            return query;
        }
        public async Task UpdateAsync(PostUpdateDto dto)
        {
            string dtoAsJson = JsonSerializer.Serialize(dto);
            StringContent body = new StringContent(dtoAsJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PatchAsync("/post", body);
            if (!response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                throw new Exception(content);
            }
        }
        public async Task<PostBasicDto> GetByIdAsync(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"/post/{id}");
            string content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }

            PostBasicDto post = JsonSerializer.Deserialize<PostBasicDto>(content,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            )!;
            return post;
        }
        public async Task DeleteAsync(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"post/{id}");
            if (!response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                throw new Exception(content);
            }
        }
    }
}
