using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ListPostsView
{
    private readonly IPostRepository postRepository;

    public ListPostsView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task ListPosts()
    {
        IQueryable<Post> posts = postRepository.GetMany();

        if (!posts.Any())
        {
            Console.WriteLine("There are no posts");
            return;
        }
        
        Console.WriteLine("Listing all posts...");
        foreach (var element in posts)
        {
            Console.WriteLine($"Title: {element.Title}, Body: {element.Body}, ID: {element.Id}, Author: {element.UserId}");
        }
        
    }
}