using CLI.UI.ManageComments;
using CLI.UI.ManagePosts;
using CLI.UI.ManageUsers;
using Entities;
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
    
    private readonly CreateCommentView createCommentView;
    private readonly ListCommentsView listCommentsView;
    private readonly ManageCommentsView manageCommentsView;
    
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

        createCommentView = new CreateCommentView(this.commentRepository);
        manageCommentsView = new ManageCommentsView(this.commentRepository);
        listCommentsView = new ListCommentsView(this.commentRepository);
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
            Console.WriteLine("7. Update Post");
            Console.WriteLine("8. Update Comment");
            Console.WriteLine("9. Update User");
            Console.WriteLine("10. Exit");
            
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Console.WriteLine("Enter User Name: ");
                    string userName = Console.ReadLine();
                    
                    Console.WriteLine("Enter Password: ");
                    string password = Console.ReadLine();
                    createUserView.AddUserAsync(userName, password);
                    break;
                
                case "2":
                    Console.WriteLine("Enter Post Title: ");
                    string postTitle = Console.ReadLine();
                    
                    Console.WriteLine("Enter Post Content: ");
                    string postContent = Console.ReadLine();
                    
                    Console.WriteLine("Enter User Id");
                    int userId = int.Parse(Console.ReadLine());
                    
                    createPostView.AddPostAsync(postTitle, postContent, userId);
                    break;
                
                case "3":
                    Console.WriteLine("Enter Comment Content: ");
                    string commentContent = Console.ReadLine();
                    
                    Console.WriteLine("Enter Post Id: ");
                    int postId = int.Parse(Console.ReadLine());
                    
                    Console.WriteLine("Enter User Id: ");
                    int userID = int.Parse(Console.ReadLine());
                    
                    createCommentView.AddCommentAsync(commentContent, postId, userID);
                    break;
                
                case "4":
                    listUsersView.ListUsers();
                    break;
                
                case "5":
                    listPostsView.ListPosts();
                    break;
                
                case "6":
                    listCommentsView.ListComments();
                    break;
                
                case "7": 
                    Console.WriteLine("Enter Post Title: ");
                    string title = Console.ReadLine();
                    
                    Console.WriteLine("Enter Post Content: ");
                    string postContent1 = Console.ReadLine();
                    
                    Console.WriteLine("Enter Post Id: ");
                    int postIds = int.Parse(Console.ReadLine());
                    await managePostsView.UpdatePostAsync(postIds, title, postContent1 );
                    break;
                
                case "8":
                    Console.WriteLine("Enter Comment Body: ");
                    string s1 = Console.ReadLine();
                    
                    Console.WriteLine("Enter Comment Id: ");
                    int commentId = int.Parse(Console.ReadLine());

                    await manageCommentsView.UpdateCommentAsync(s1, commentId);
                    break;
                
                case "9":
                    Console.WriteLine("Enter User Name: ");
                    string userNameToChange = Console.ReadLine();
                    
                    Console.WriteLine("Enter Password: ");
                    string passwordToChange = Console.ReadLine();
                    
                    Console.WriteLine("Enter User ID: ");
                    int userIdToChange = int.Parse(Console.ReadLine());
                    
                    await manageUsersView.UpdateUserAsync(userNameToChange, passwordToChange, userIdToChange);
                    break;
                
                case "10":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
        Console.WriteLine("");
    }
}