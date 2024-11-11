using System.Text.Json;
using DTOs;

namespace BlazorApp.Services;

public class HttpUserService : IUserService
{
    private readonly HttpClient httpClient;

    public HttpUserService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    
    public async Task<UserDTO> addUserAsync(CreateUserDTO request)
    {
        HttpResponseMessage httpResponse = await httpClient.PostAsJsonAsync("/users", request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<UserDTO>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        
    }

    public async Task<UserDTO> updateUserAsync(int userId, CreateUserDTO request)
    {
        HttpResponseMessage httpResponse = await httpClient.PutAsJsonAsync($"/users/{userId}", request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<UserDTO>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task<UserDTO> getUserByIdAsync(int userId)
    {
        var user = await httpClient.GetFromJsonAsync<UserDTO>($"/users/{userId}");

        return user;
    }

    public async Task deleteUserAsync(int userId)
    {
        HttpResponseMessage httpResponse = await httpClient.DeleteAsync($"users/{userId}");
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
    }

    public async Task<List<UserDTO>> getAllUsers(string? username)
    {
        var uri = string.IsNullOrEmpty(username) ? "/users" : $"/users?username={username}";
        
        HttpResponseMessage httpResponse = await httpClient.GetAsync(uri);
        
        string response = await httpResponse.Content.ReadAsStringAsync();

        if (httpResponse.IsSuccessStatusCode)
        {
            List<UserDTO>? users = JsonSerializer.Deserialize<List<UserDTO>>(response,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return users;
        }
        else
        {
            throw new Exception(response);
        }
        
    }
}