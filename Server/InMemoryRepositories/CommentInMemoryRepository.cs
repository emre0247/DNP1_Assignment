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
        // We check if the post id already exists
        comment.Id = comments.Any() 
            // If the post id already exists we find the max id in the list + 1
            ? comments.Max(x => x.Id) + 1
            // If there isn't any posts in the list we assign the id to 1
            : 1;
        comments.Add(comment);
        return Task.FromResult(comment);
    }

    public Task UpdateAsync(Comment comment)
    {
        // We look for the existing post that we want to update
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
        throw new NotImplementedException();
    }

    public Task<Comment> GetSingleAsync(int id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Comment> GetMany()
    {
        throw new NotImplementedException();
    }
}