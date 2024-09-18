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
    
    public async Task AddUserAsync(string name, string password)
    {
        // Verify its not empty
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
        {
            Console.WriteLine("Name and password cannot be empty!");
            return;
        }
        
        while (userRepository.GetMany().Any(x => x.Username == name))
        {
            Console.WriteLine("Username already exists!");
            Console.WriteLine("Please enter a different username!");
            name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Username cannot be empty!");
                Console.WriteLine("Please enter a different username!");
            }
        }
        
        // We create the user
        var user = new User
        {
            Username = name,
            Password = password,
        };
        
        User createdUser = await userRepository.AddAsync(user);
        Console.WriteLine($"User {createdUser.Username} created with id {createdUser.Id}");
        
    }
}