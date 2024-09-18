using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class CreateCommentView
{
    private readonly ICommentRepository commentRepository;

    public CreateCommentView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    public async Task AddCommentAsync(string body, int postId, int userId)
    {
        if (string.IsNullOrEmpty(body))
        {
            Console.WriteLine("Comment body is required");
            return;
        }
        
        
        var creatingComment = new Comment
        {
            Body = body,
            PostId = postId,
            UserId = userId
        };
        
        Comment createdComment = await commentRepository.AddAsync(creatingComment);
        Console.WriteLine($"Created comment {createdComment.Body} - {createdComment.Id}");
        
        
    }
}