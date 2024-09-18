using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class ManageCommentsView
{
    private readonly ICommentRepository commentRepository;

    public ManageCommentsView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    public async Task UpdateCommentAsync(string body, int commentId)
    {
        var comment = await commentRepository.GetSingleAsync(commentId);
        
        comment.Body = body;
        await commentRepository.UpdateAsync(comment);
        
        Console.WriteLine("Comment updated");
    }
}