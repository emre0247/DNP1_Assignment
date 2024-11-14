using System.Net;
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
        try
        {
            HttpResponseMessage httpResponse = await httpClient.PostAsJsonAsync("/users", request);
            string response = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception(response);
            }
            return JsonSerializer.Deserialize<UserDTO>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"HTTP request error: {ex.Message}", ex);

        }
        catch (JsonException ex)
        {
            throw new Exception("Error deserializing the response.", ex);
        }
        
    }

    public async Task<UserDTO> updateUserAsync(int userId, CreateUserDTO request)
    {
        try
        {
            HttpResponseMessage httpResponse = await httpClient.PutAsJsonAsync($"/users/{userId}", request);
            string response = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception(response);
            }
            return JsonSerializer.Deserialize<UserDTO>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"HTTP request error: {ex.Message}", ex);
        }
        catch (JsonException ex)
        {
            throw new Exception("Error deserializing the response.", ex);
        }
    }

    public async Task<UserDTO> getUserByIdAsync(int userId)
    {
            HttpResponseMessage httpResponse = await httpClient.GetAsync($"/users/{userId}");
            string response = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception(response);
            }
            return JsonSerializer.Deserialize<UserDTO>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        
    }

    public async Task deleteUserAsync(int userId)
    {
        
        HttpResponseMessage httpResponse = await httpClient.DeleteAsync($"users/{userId}");
        
        if (!httpResponse.IsSuccessStatusCode)
        {
            string response = await httpResponse.Content.ReadAsStringAsync();
            throw new Exception(response);
        }
    }

    public async Task<List<UserDTO>> getAllUsers(string? username)
    {
        try
        {
            var uri = string.IsNullOrEmpty(username) ? "/users" : $"/users?username={username}";
            HttpResponseMessage httpResponse = await httpClient.GetAsync(uri);
            string response = await httpResponse.Content.ReadAsStringAsync();

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error retrieving users: {response}");
            }
            List<UserDTO>? users = JsonSerializer.Deserialize<List<UserDTO>>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return users ?? new List<UserDTO>();
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"HTTP request error: {ex.Message}", ex);
        }
        catch (JsonException ex)
        {
            throw new Exception("Error deserializing the response.", ex);
        }
        
    }
}