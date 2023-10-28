using Models.DTO;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Text.Json;
using HttpClients.ClientInterfaces;
using System.Security.Claims;
using System.Net.Http.Headers;

namespace HttpClients.Implementations
{
    public class UserHttpClient : IUserService
    {
        private readonly HttpClient client;

        // this private variable for simple caching
        public static string? Jwt { get; private set; } = "";

        public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } = null!;

        public UserHttpClient(HttpClient client)
        {
            this.client = client;
        }


        public async Task<User> Create(UserCreationDto dto)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("/users", dto);
            string result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(result);
            }

            User user = JsonSerializer.Deserialize<User>(result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
            return user;
        }
        public async Task<IEnumerable<User>> GetUsers(string? usernameContains = null)
        {
            string uri = "/users";
            if (!string.IsNullOrEmpty(usernameContains))
            {
                uri += $"?username={usernameContains}";
            }
            HttpResponseMessage response = await client.GetAsync(uri);
            string result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(result);
            }

            IEnumerable<User> users = JsonSerializer.Deserialize<IEnumerable<User>>(result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
            return users;
        }

        public async Task LoginAsync(AuthUserDTO dto)
        {
         

            string userAsJson = JsonSerializer.Serialize(dto);
            StringContent content = new(userAsJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("/users/login", content);
            string responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(responseContent);
            }

            string token = responseContent;
            Jwt = token;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Jwt);

            ClaimsPrincipal principal = CreateClaimsPrincipal();

            OnAuthStateChanged.Invoke(principal);
        }
        public Task LogoutAsync()
        {
            Jwt = null;
            client.DefaultRequestHeaders.Authorization = null;
           ClaimsPrincipal principal = new();
            OnAuthStateChanged.Invoke(principal);
            return Task.CompletedTask;
        }

        public Task<ClaimsPrincipal> GetAuthAsync()
        {
            ClaimsPrincipal principal = CreateClaimsPrincipal();
            return Task.FromResult(principal);
        }
        private static ClaimsPrincipal CreateClaimsPrincipal()
        {
            if (string.IsNullOrEmpty(Jwt))
            {
                return new ClaimsPrincipal();
            }

            IEnumerable<Claim> claims = ParseClaimsFromJwt(Jwt);

            ClaimsIdentity identity = new(claims, "jwt");

            ClaimsPrincipal principal = new(identity);
            return principal;
        }

        // Below methods stolen from https://github.com/SteveSandersonMS/presentation-2019-06-NDCOslo/blob/master/demos/MissionControl/MissionControl.Client/Util/ServiceExtensions.cs
        private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            string payload = jwt.Split('.')[1];
            byte[] jsonBytes = ParseBase64WithoutPadding(payload);
            Dictionary<string, object>? keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs!.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!));
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2:
                    base64 += "==";
                    break;
                case 3:
                    base64 += "=";
                    break;
            }

            return Convert.FromBase64String(base64);
        }
    }
}
