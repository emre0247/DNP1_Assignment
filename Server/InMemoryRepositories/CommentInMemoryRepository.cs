using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class CommentInMemoryRepository : ICommentRepository
{
    private List<Comment> comments;

    public CommentInMemoryRepository()
    {
        comments = new List<Comment>();
    }
    public Task<Comment> AddAsync(Comment comment)
    {
        // We check if the comment id already exists
        comment.Id = comments.Any() 
            // If the comment id already exists we find the max id in the list + 1
            ? comments.Max(x => x.Id) + 1
            // If there isn't any comments in the list we assign the id to 1
            : 1;
        comments.Add(comment);
        return Task.FromResult(comment);
    }

    public Task UpdateAsync(Comment comment)
    {
        // We look for the existing comment that we want to update
        Comment? existingComment = comments.SingleOrDefault(p => p.Id == comment.Id);
        if (existingComment is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{comment.Id}' not found");
        }

        comments.Remove(existingComment);
        comments.Add(existingComment);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        // Search for the comment with the specified ID. If no matching comment is found, commentToDelete will be null.
        Comment? commentToDelete = comments.SingleOrDefault(p => p.Id == id);
        
        // If no post is found (commentToDelete is null), throw an exception indicating that the comment was not found.
        if (commentToDelete is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found"); // Exception message includes the ID for context.
        }
       
        // Remove the found comment from the collection.
        comments.Remove(commentToDelete);
        // Return a completed task to indicate that the operation has finished (synchronous method in async pattern).
        return Task.CompletedTask;
    }

    public Task<Comment> GetSingleAsync(int id)
    {
        Comment? commentToGet = comments.SingleOrDefault(p => p.Id == id);
        if (commentToGet is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found");
        }
        return Task.FromResult(commentToGet);
    }

    public IQueryable<Comment> GetMany()
    { 
        return comments.AsQueryable();
    }
}