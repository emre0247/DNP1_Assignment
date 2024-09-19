using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class ListCommentsView
{
    private readonly ICommentRepository commentRepository;

    public ListCommentsView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    public void ListComments()
    {
        IQueryable<Comment> comments = commentRepository.GetMany();

        if (!comments.Any())
        {
            Console.WriteLine("There are no comments");
            return;
        }
        
        Console.WriteLine("Listing comments...");
        foreach (var element in comments)
        {
            Console.WriteLine($"Body: {element.Body}, ID: {element.Id}, Post: {element.PostId}, Author: {element.UserId}");
        }
    }
}