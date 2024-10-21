using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class CreateUserView
{
    private readonly IUserRepository userRepository;

    public CreateUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    
    public async Task AddUserAsync()
    {
        Console.WriteLine("Adding new user");
        Console.WriteLine("----------------");
        string name = null;
        string password = null;

        while (string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(name))
        {
            Console.WriteLine("Please enter a username: ");
            name = Console.ReadLine();
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(name) )
            {
                Console.WriteLine("Please enter a valid username");
            }
            
            while (userRepository.GetMany().Any(x => x.Username == name))
            {
                Console.WriteLine("Username already exists!");
                Console.WriteLine("Please enter a different username!");
                name = Console.ReadLine();
            }
        }

        while (string.IsNullOrWhiteSpace(password) || string.IsNullOrEmpty(password))
        {
            Console.WriteLine("Please enter a password: ");
            password = Console.ReadLine();

            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Please enter a valid password");
            }
        }
        
       
        
        // We create the user
        User user = new User
        {
            Username = name,
            Password = password,
        };
        
        User createdUser = await userRepository.AddAsync(user);
        Console.WriteLine($"User {createdUser.Username} created with password {createdUser.Password} and id {createdUser.Id}");
        
    }
}