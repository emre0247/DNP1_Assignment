using System.Text.Json;
using Entities;
using RepositoryContracts;
using InvalidOperationException = System.InvalidOperationException;

namespace FileRepositories;

public class CommentFileRepository : ICommentRepository
{
    private readonly string filePath = "comments.json";


    public CommentFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }
    public async Task<Comment> AddAsync(Comment comment)
    {
        string commentAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentAsJson)!;
        int maxId = comments.Count > 0 ? comments.Max(x => x.Id) : 1;
        comment.Id = maxId + 1;
        comments.Add(comment);
        commentAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentAsJson);
        return comment;
    }

    public async Task UpdateAsync(Comment comment)
    {
        string commentAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentAsJson)!;
        Comment? existingComment = comments.SingleOrDefault(p => p.Id == comment.Id);
        if (existingComment is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{comment.Id}' not found");
        }
        
        comments.Remove(existingComment);
        comments.Add(comment);
        commentAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentAsJson);
    }

    public async Task DeleteAsync(int id)
    {
        string commentAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentAsJson)!;
        Comment? commentToDelete = comments.SingleOrDefault(p => p.Id == id);
        
        if (commentToDelete is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{id}' not found");
        }

        comments.Remove(commentToDelete);
        commentAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentAsJson);
    }

    public async Task<Comment> GetSingleAsync(int id)
    {
        string commentAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentAsJson)!;
        Comment? comment = comments.SingleOrDefault(p => p.Id == id);

        if (comment is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{id}' not found");
        }
        
        commentAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentAsJson);

        return comment;
    }

    public IQueryable<Comment> GetMany()
    {
        string commentAsJson = File.ReadAllTextAsync(filePath).Result;
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentAsJson)!;
        return comments.AsQueryable();
    }
}