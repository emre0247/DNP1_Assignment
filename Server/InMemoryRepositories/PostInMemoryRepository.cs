using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class PostInMemoryRepository : IPostRepository
{
    private List<Post> posts;

    public PostInMemoryRepository()
    {
        posts = new List<Post>();
    }

    public Task<Post> AddAsync(Post post)
    {
        // We check if the post id already exists
        post.Id = posts.Any()
        // If the post id already exists we find the max id in the list + 1
            ? posts.Max(x => x.Id) + 1
        // If there isn't any posts in the list we assign the id to 1
            : 1;
        posts.Add(post);
        return Task.FromResult(post);
    }

    public Task UpdateAsync(Post post)
    {
        // We look for the existing post that we want to update
        Post? existingPost = posts.SingleOrDefault(p => p.Id == post.Id);
        if (existingPost is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{post.Id}' not found");
        }

        posts.Remove(existingPost);
        posts.Add(post);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        // Search for the post with the specified ID. If no matching post is found, postToDelete will be null.
        Post? postToDelete = posts.SingleOrDefault(p => p.Id == id);
        
        // If no post is found (postToDelete is null), throw an exception indicating that the post was not found.
        if (postToDelete is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found"); // Exception message includes the ID for context.
        }
       
        // Remove the found post from the collection.
        posts.Remove(postToDelete);
        // Return a completed task to indicate that the operation has finished (synchronous method in async pattern).
        return Task.CompletedTask;
    }

    public Task<Post> GetSingleAsync(int id)
    {
        Post? postToGet = posts.SingleOrDefault(p => p.Id == id);
        if (postToGet is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found");
        }
        return Task.FromResult(postToGet);
    }

    public IQueryable<Post> GetMany()
    {
        return posts.AsQueryable();
    }
}