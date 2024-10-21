using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class SingleUserView
{
    private readonly IUserRepository userRepository;

    public SingleUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task SingleUserAsync()
    {
        Console.WriteLine("Getting single user");
        Console.WriteLine("------------------");

        while (true)
        {
            
                Console.WriteLine("Enter userId: ");
                int userId = int.Parse(Console.ReadLine());
            
                try
                {
                User? user = await userRepository.GetByIdAsync(userId);

                if (user is null)
                {
                    Console.WriteLine($"No such user: {userId}");
                    Console.WriteLine("Try again");
                }
                else
                {
                    Console.WriteLine($"Username: {user.Username}, Password: {user.Password}, UserId: {userId}");
                    break;
                }
            }
                
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Try again");
            }
        }
        
        
    }
}