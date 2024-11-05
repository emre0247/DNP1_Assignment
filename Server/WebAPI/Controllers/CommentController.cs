using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository commentRepository;

    public CommentController(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }
    
    
    //Method to create a new comment
    [HttpPost]
    public async Task<ActionResult<CommentDTO>> CreateComment([FromBody]CreateCommentDTO request)
    {
        Comment comment = new()
        {
            PostId = request.PostId,
            UserId = request.UserId,
            Body = request.Body,
        };

        Comment created = await commentRepository.AddAsync(comment);

        CommentDTO dto = new()
        {
            Id = created.Id,
            Body = created.Body,
            UserId = created.UserId,
            PostId = created.PostId
        };

        return Ok(dto);
    }
    
    // Method to get comments based on userid or Post id
    [HttpGet]
    public async Task<ActionResult<List<CommentDTO>>> GetComments([FromQuery] int? userId, [FromQuery] int? postId)
    {
        var comments = commentRepository.GetMany();
        if (userId.HasValue)
        {
            comments = comments.Where(c => c.UserId == userId.Value);
        }

        if (postId.HasValue)
        {
            comments = comments.Where(c => c.PostId == postId.Value);
        }

        var commentDto = comments.Select(c => new CommentDTO()
        {
            Body = c.Body,
            Id = c.Id,
            PostId = c.PostId,
            UserId = c.UserId
        }).ToList();

        return Ok(commentDto);
    }
    
    // Method to get comment based on its id
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CommentDTO>> GetComment(int id)
    {
        var comment = await commentRepository.GetSingleAsync(id);

        CommentDTO dto = new()
        {
            Id = comment.Id,
            Body = comment.Body,
            PostId = comment.PostId,
            UserId = comment.UserId
        };
        
        return Ok(dto);
    }
    
    // Method to delete comment based on Id
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<CommentDTO>> DeleteComment(int id)
    {
        await commentRepository.DeleteAsync(id);
        return Ok("Comment deleted");
    }
    
    // Method to update existing comment
    [HttpPut("{id:int}")]
    public async Task<ActionResult<CommentDTO>> UpdateComment(int id, [FromBody] CreateCommentDTO request)
    {
        var existingComment = await commentRepository.GetSingleAsync(id);
        if (existingComment is null)
        {
            return NotFound("Comment not found");
        }
        
        existingComment.Body = request.Body;
        existingComment.UserId = request.UserId;
        existingComment.PostId = request.PostId;
        
        await commentRepository.UpdateAsync(existingComment);

        CommentDTO dto = new()
        {
            Id = existingComment.Id,
            Body = existingComment.Body,
            UserId = existingComment.UserId,
        };

        return Ok(dto);
    }
}