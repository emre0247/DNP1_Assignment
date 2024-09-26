using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class SinglePostView
{
    private readonly IPostRepository postRepository;

    public SinglePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task GetSinglePost()
    {
        Console.WriteLine("Getting single post...");
        Console.WriteLine("-------------------------");
        while (true)
        {
            Console.WriteLine("Enter post id: ");
            int postId = int.Parse(Console.ReadLine());
        
            Post? postToGet = await postRepository.GetSingleAsync(postId);
            
            if (postToGet is null)
            {
                Console.WriteLine($"No post found with id {postId}");
                Console.WriteLine("Try again");
            }
            else
            {
                Console.WriteLine($"Post Body: {postToGet.Body} - Post Id: {postToGet.Id} - Post Title: {postToGet.Title}");
                break;
            }
            
        }
    }
}