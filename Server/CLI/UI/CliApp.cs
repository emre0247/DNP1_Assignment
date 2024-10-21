using CLI.UI.ManageComments;
using CLI.UI.ManagePosts;
using CLI.UI.ManageUsers;
using Entities;
using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{
    

    //private readonly ManagePostsView managePostsView = new ManagePostsView();
    private readonly IUserRepository userRepository;
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;
    
    public CliApp(IUserRepository userRepository, ICommentRepository commentRepository, IPostRepository postRepository)
    {
        this.userRepository = userRepository;
        this.commentRepository = commentRepository;
        this.postRepository = postRepository;
    }

    public async Task StartAsync()
    {
        Console.WriteLine("Starting CLI App... ");
        await OptionBasedView();
    }

    private async Task OptionBasedView()
    {
        bool running = true;
        while (running)
        {
        Console.WriteLine("Welcome to the Application!");
        Console.WriteLine("---------------------------");
        Console.WriteLine("Please choose an option: ");
        Console.WriteLine("1. Manage Posts (Type 'post')");
        Console.WriteLine("2. Manage Comments (Type 'comment')");
        Console.WriteLine("3. Manage Users (Type 'user')");
        Console.WriteLine("To exit the application, type 'exit'.");
        Console.WriteLine("Your choice: ");
        string operation = Console.ReadLine().ToLower();
        
            switch (operation)
            {
                case "post":
                    await ManagePostsAsync();
                    break;
                
                case "comment":
                    break;
                
                case "user":
                    await ManageUsersAsync();
                    break;
            }
        }
    }

    private async Task ManageUsersAsync()
    {
        CreateUserView createUserView = new CreateUserView(userRepository);
        ListUsersView listUsersView = new ListUsersView(userRepository);
        SingleUserView singleUserView = new SingleUserView(userRepository);
        ManageUsersView manageUsersView = new ManageUsersView(createUserView, listUsersView, singleUserView);
        await manageUsersView.ShowManageUserAsync();
    }

    private async Task ManagePostsAsync()
    {
        CreatePostView createPostView = new CreatePostView(postRepository, userRepository);
        ListPostsView listPostsView = new ListPostsView(postRepository);
        SinglePostView singlePostView = new SinglePostView(postRepository);
        UpdatePostView updatePostView = new UpdatePostView(postRepository);
        ManagePostsView managePostsView = new ManagePostsView(createPostView, listPostsView, singlePostView, updatePostView);
        await managePostsView.ShowManagePostsAsync();
    }
}