using DTOs;

namespace BlazorApp.Services;

public interface IPostService
{
    public Task<PostDTO> addPostAsync(CreatePostDTO request);
    
}