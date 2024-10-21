using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ListUsersView
{
    private readonly IUserRepository userRepository;

    public ListUsersView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public void ListUsers()
    {
        var users = userRepository.GetMany();
        
        if (!users.Any())
        {
            Console.WriteLine("There are no users");
            return;
        }
        
        Console.WriteLine("Users: ");
        foreach (var element in users)
        {
            Console.WriteLine($"ID: {element.Id}, Username: {element.Username}");
        }
    }
    
}