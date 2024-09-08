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
        
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Post> GetSingleAsync(int id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Post> GetMany()
    {
        throw new NotImplementedException();
    }
}