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
        
        // We create the user
        var user = new User
        {
            Username = name,
            Password = password,
            Id = 1
        };
        
        if (userRepository.GetMany().Where(x => user.Username == x.Username).Any())
        {
            Console.WriteLine("Username already exists!");
            Console.WriteLine("Please enter a different username!");
            string username = Console.ReadLine();
            user.Username = username;
        }
        User createdUser = await userRepository.AddAsync(user);
        Console.WriteLine($"User {createdUser.Username} created with id {createdUser.Id}");
        
    }
}