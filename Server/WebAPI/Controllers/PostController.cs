using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostRepository postRepository;

    public PostController(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    // Method to create a new post
    [HttpPost]
    public async Task<ActionResult<Post>> AddPost([FromBody] CreatePostDTO request)
    {
        Post post = new()
        {
            Title = request.Title,
            Body = request.Body,
            UserId = request.UserId,
        };

        Post created = await postRepository.AddAsync(post);

        PostDTO dto = new()
        {
            Id = created.Id,
            Body = created.Body,
            Title = created.Title,
            UserId = created.UserId,
        };
        
        return dto is not null ? Ok(dto) : BadRequest("Try again");
    }

    // Method to get posts based on title or userId
    [HttpGet]
    public async Task<ActionResult<List<Post>>> GetPosts([FromQuery] string? title, [FromQuery] int? userId)
    {
        var posts = postRepository.GetMany();

        if (!string.IsNullOrEmpty(title))
        {
            posts = posts.Where(p => p.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
        }

        if (userId.HasValue)
        {
            posts = posts.Where(p => p.UserId == userId);
        }

        var postDto = posts.Select(p => new PostDTO()
        {
            Id = p.Id,
            UserId = p.UserId,
            Title = p.Title,
            Body = p.Body,
        }).ToList();
        
        return postDto is not null ? Ok(postDto) : BadRequest("No matching posts found");
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Post>> GetPost(int id)
    {

        try
        {
            var post = await postRepository.GetSingleAsync(id);

            PostDTO dto = new PostDTO
            {
                Id = post.Id,
                Body = post.Body,
                Title = post.Title,
                UserId = post.UserId
            };
            return Ok(dto);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    // Method to delete user based on Id
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Post>> DeletePost(int id)
    {
        await postRepository.DeleteAsync(id);
        return Ok("Post deleted");
    }
    
    // Method to update existing post
    [HttpPut("{id:int}")]
    public async Task<ActionResult<Post>> UpdatePost(int id, [FromBody] CreatePostDTO request)
    {
        var existingPost = await postRepository.GetSingleAsync(id);

        if (existingPost is null)
        {
            return NotFound();
        }
        
        existingPost.Body = request.Body;
        existingPost.Title = request.Title;
        existingPost.UserId = request.UserId;
        
        await postRepository.UpdateAsync(existingPost);

        PostDTO dto = new PostDTO()
        {
            Id = existingPost.Id,
            Body = existingPost.Body,
            Title = existingPost.Title,
            UserId = existingPost.UserId,
        };

        return Ok(dto);
    }

}