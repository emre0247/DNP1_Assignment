using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ManageUsersView
{
    private readonly CreateUserView createUserView;
    private readonly ListUsersView listUsersView;

    public ManageUsersView(CreateUserView createUserView, ListUsersView listUsersView)
    {
        this.createUserView = createUserView;
        this.listUsersView = listUsersView;
    }

    public async Task ShowManageUserAsync()
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine("Manage Users Menu");
            Console.WriteLine("-------------------------");
            Console.WriteLine("Please choose an option: ");
            Console.WriteLine("1. Create a user (Type 'Create')");
            Console.WriteLine("2. List all user (Type 'List')");
            Console.WriteLine("3. List single user (Type 'Single')");
            Console.WriteLine("4. Update a user (Type 'Update')");
            //More to come...
            
            string input = Console.ReadLine().ToLower();
            switch (input)
            {
                case "create":
                    await createUserView.AddUserAsync();
                    break;
                
                case "list":
                    listUsersView.ListUsers();
                    break;
                
                case "single":
                    //await singlePostView.GetSinglePost();
                    break;
                case "update":
                    //await updatePostView.UpdatePostAsync();
                    break;
            }
            
        }
    }
}