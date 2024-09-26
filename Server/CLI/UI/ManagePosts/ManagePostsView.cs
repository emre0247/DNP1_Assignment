using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ManagePostsView
{
    
    private readonly CreatePostView createPostView;
    private readonly ListPostsView listPostsView;
    private readonly SinglePostView singlePostView;
    private readonly UpdatePostView updatePostView;

    public ManagePostsView(CreatePostView createPostView, ListPostsView listPostsView, SinglePostView singlePostView, UpdatePostView updatePostView)
    {
        this.createPostView = createPostView;
        this.listPostsView = listPostsView;
        this.singlePostView = singlePostView;
        this.updatePostView = updatePostView;
    }

    public async Task ShowManagePostsAsync()
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine("Manage Posts Menu");
            Console.WriteLine("-------------------------");
            Console.WriteLine("Please choose an option: ");
            Console.WriteLine("1. Create a post (Type 'Create')");
            Console.WriteLine("2. List all posts (Type 'List')");
            Console.WriteLine("3. List single posts (Type 'Single')");
            Console.WriteLine("4. Update a post (Type 'Update')");
            //More to come...
            
            string input = Console.ReadLine().ToLower();
            switch (input)
            {
                case "create":
                    await createPostView.AddPostAsync();
                    break;
                
                case "list":
                    await listPostsView.ListPosts();
                    break;
                
                case "single":
                    await singlePostView.GetSinglePost();
                    break;
                case "update":
                    await updatePostView.UpdatePostAsync();
                    break;
            }
            
        }
    }

    
}