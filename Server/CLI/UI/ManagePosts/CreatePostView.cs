using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class CreatePostView
{
    private readonly IPostRepository postRepository;
    
    private readonly IUserRepository userRepository;
    

    public CreatePostView(IPostRepository postRepository, IUserRepository userRepository)
    {
        this.postRepository = postRepository;
        this.userRepository = userRepository;
    }

    public async Task AddPostAsync()
    {
        Console.WriteLine("Adding new post");
        Console.WriteLine("----------------");

        string title = null;
        string body = null;
        int userId; 
       
        while (string.IsNullOrEmpty(title) || string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Enter the title of your post: ");
            title = Console.ReadLine();

            if (string.IsNullOrEmpty(title) || string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("The title cannot be null, empty or whitespace.");
            }
        }

        while (string.IsNullOrEmpty(body) || string.IsNullOrWhiteSpace(body))
        {
            Console.WriteLine("Enter the body of your post: ");
            body = Console.ReadLine();
            
            if (string.IsNullOrEmpty(body) || string.IsNullOrWhiteSpace(body))
            {
                Console.WriteLine("The body cannot be null, empty or whitespace.");
            }
        }

        bool isUserIdValid = false;
        do
        {
            Console.WriteLine("Enter your User Id: ");
            string userIdt = Console.ReadLine();
            if (int.TryParse(userIdt, out userId))
            {
                try
                {
                    isUserIdValid = await IsUserIdValid(userId);
                    
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                if (!isUserIdValid)
                {
                    Console.WriteLine("User ID not found. Please enter a valid User ID.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a numeric User ID.");
            }
        } while (!isUserIdValid);
        
        
        // We create the post
        Post postToCreate = new Post
        {
            Title = title,
            Body = body,
            UserId = userId,
        };
        
        Post post = await postRepository.AddAsync(postToCreate);
        Console.WriteLine($"Created post: Title: {post.Title} - Body: {post.Body} - UserId: {post.UserId}");
    }

    private async Task<bool> IsUserIdValid(int id)
    {
        var user = await userRepository.GetByIdAsync(id);
        return user != null;
    }
    
}