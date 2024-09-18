using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class CreatePostView
{
    private readonly IPostRepository postRepository;

    public CreatePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task AddPostAsync(string title, string body, int userId)
    {
        if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(body))
        {
            Console.WriteLine("The title and body cannot be null or empty!");
            return;
        }
        
        // We create the post
        var postToCreate = new Post
        {
            Title = title,
            Body = body,
            UserId = userId,
        };
        
        Post post = await postRepository.AddAsync(postToCreate);
        Console.WriteLine($"Created post: {post.Title} - {post.Body}");
    }
    
}