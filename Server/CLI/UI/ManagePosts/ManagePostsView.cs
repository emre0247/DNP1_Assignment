using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ManagePostsView
{
    
    private readonly CreatePostView createPostView;
    private readonly ListPostsView listPostsView;
    private readonly SinglePostView singlePostView;

    public ManagePostsView(CreatePostView createPostView, ListPostsView listPostsView, SinglePostView singlePostView)
    {
        this.createPostView = createPostView;
        this.listPostsView = listPostsView;
        this.singlePostView = singlePostView;
    }

    public async Task ShowManagePostsAsync()
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine("You selected Manage Posts");
            Console.WriteLine("-------------------------");
            Console.WriteLine("Please choose an option: ");
            Console.WriteLine("1. Create a post (Type 'Create')");
            Console.WriteLine("2. List all posts (Type 'List')");
            Console.WriteLine("3. List single posts (Type 'Single')");
            //More to come...
            
            string input = Console.ReadLine().ToLower();
            switch (input)
            {
                case "create":
                    await createPostView.AddPostAsync();
                    break;
                
                case "list":
                    listPostsView.ListPosts();
                    break;
                
                case "single":
                    await singlePostView.GetSinglePost();
                    break;
            }
            
        }
    }

    
}