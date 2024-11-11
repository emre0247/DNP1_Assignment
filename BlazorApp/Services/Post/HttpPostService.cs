using System.Text.Json;
using DTOs;

namespace BlazorApp.Services;

public class HttpPostService : IPostService
{
    
    private readonly HttpClient httpClient;

    public HttpPostService(HttpClient client)
    {
        this.httpClient = client;
    }
    public async Task<PostDTO> addPostAsync(CreatePostDTO request)
    {
        HttpResponseMessage httpResponse = await httpClient.PostAsJsonAsync("api/post", request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }

        return JsonSerializer.Deserialize<PostDTO>(response,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}