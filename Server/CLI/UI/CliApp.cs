using CLI.UI.ManagePosts;
using CLI.UI.ManageUsers;
using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{
    private readonly IUserRepository userRepository;
    private readonly ICommentRepository commentRepository;
    private readonly IPostRepository postRepository;

    private readonly CreatePostView createPostView;
    private readonly ListPostsView listPostsView;
    private readonly ManagePostsView managePostsView;
    private readonly SinglePostView singlePostView;
    
    private readonly CreateUserView createUserView;
    private readonly ListUsersView listUsersView;
    private readonly ManageUsersView manageUsersView;
    
    public CliApp(IUserRepository userRepository, ICommentRepository commentRepository, IPostRepository postRepository)
    {
        this.userRepository = userRepository;
        this.commentRepository = commentRepository;
        this.postRepository = postRepository;

        createPostView = new CreatePostView(this.postRepository);
        listPostsView = new ListPostsView(this.postRepository);
        managePostsView = new ManagePostsView(this.postRepository);
        singlePostView = new SinglePostView(this.postRepository);
        
        createUserView = new CreateUserView(this.userRepository);
        listUsersView = new ListUsersView(this.userRepository);
        manageUsersView = new ManageUsersView(this.userRepository);

    }

    public async Task StartAsync()
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Add User");
            Console.WriteLine("2. Add Post");
            Console.WriteLine("3. Add Comment");
            
            Console.WriteLine("4. View Users");
            Console.WriteLine("5. View Posts");
            Console.WriteLine("6. View Comments");
            Console.WriteLine("7. Exit");
            
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Console.WriteLine("Enter User Name: ");
                    string userName = Console.ReadLine();
                    
                    Console.WriteLine("Enter Password: ");
                    string password = Console.ReadLine();
                    createUserView.AddUserAsync(userName, password);
                    break;}
        }
        Console.WriteLine("");
    }
}