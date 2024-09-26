using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class UpdatePostView
{
    private readonly IPostRepository postRepository;

    public UpdatePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task UpdatePostAsync()
    {
        Console.WriteLine("Update Post...");
        Console.WriteLine("----------------");

        while (true)
        {
            Console.WriteLine("Enter Post id to update: ");
            
            while (true)
            {
                Console.WriteLine("Enter Post id to update: ");
                if (!int.TryParse(Console.ReadLine(), out int postId))
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer for the Post ID.");
                    continue;
                }

                try
                {
                    Post post = await postRepository.GetSingleAsync(postId);
                    Console.WriteLine("Found post: ");
                    Console.WriteLine($"Title: {post.Title}");
                    Console.WriteLine($"Body: {post.Body}");
                    Console.WriteLine($"User Id: {post.UserId}");
                    Console.WriteLine($"Post Id: {postId}");
                    
                    Console.WriteLine();
                    Console.WriteLine("What do you want to change? ");
                    Console.WriteLine("1. Title (Enter 'Title')");
                    Console.WriteLine("2. Body (Enter 'Body')");
                    string option = Console.ReadLine().ToLower();
                    
                    while (true)
                    {
                        if (option.Equals("title"))
                        {
                            Console.WriteLine("Enter new title: ");
                            string newTitle = Console.ReadLine();
                            
                            if (!string.IsNullOrEmpty(newTitle) || !string.IsNullOrWhiteSpace(newTitle))
                            {
                                post.Title = newTitle;
                                await postRepository.UpdateAsync(post);
                            }

                            Console.WriteLine($"Updated post {post.Id}");
                            break;
                        }
                    }
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine($"No post found with id {postId}");
                    Console.WriteLine("Try again");
                }
            }
            }
        }
    }
