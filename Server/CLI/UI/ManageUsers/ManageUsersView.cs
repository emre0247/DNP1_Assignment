using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ManageUsersView
{
    private readonly IUserRepository userRepository;

    public ManageUsersView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task UpdateUserAsync(string username, string password, int id)
    {
        var userToChange = await userRepository.GetByIdAsync(id);

        string oldUsername = userToChange.Username;
        string oldPassword = userToChange.Password;
        
        userToChange.Username = username;
        userToChange.Password = password;
        
        await userRepository.UpdateAsync(userToChange);
        
        Console.WriteLine($"User {oldUsername} was updated to {userToChange.Username} with password {oldPassword} to {userToChange.Password}.");
    }
}