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

    public Task<UserDTO> getUserByIdAsync(int userId)
    {
        return httpClient.GetFromJsonAsync<UserDTO>($"/users/{userId}");
    }
}