using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class UserInMemoryRepository : IUserRepository
{
    private List<User> users;

    public UserInMemoryRepository()
    {
        users = new List<User>();
    }

    public Task<User> AddAsync(User user)
    {
        // We check if the user id already exists
        user.Id = users.Any()
            // If the user id already exists we find the max id in the list + 1
            ? users.Max(x => x.Id) + 1
            // If there isn't any user in the list we assign the id to 1
            : 1;
        users.Add(user);
        return Task.FromResult(user);
    }

    public Task UpdateAsync(User user)
    {
        // We look for the existing user that we want to update
        User? existingUser = users.SingleOrDefault(p => p.Id == user.Id);
        if (existingUser is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{user.Id}' not found");
        }

        users.Remove(existingUser);
        users.Add(user);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        // Search for the user with the specified ID. If no matching user is found, userToDelete will be null.
        User? userToDelete = users.SingleOrDefault(p => p.Id == id);
        
        // If no user is found (userToDelete is null), throw an exception indicating that the user was not found.
        if (userToDelete is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found"); // Exception message includes the ID for context.
        }
       
        // Remove the found user from the collection.
        users.Remove(userToDelete);
        // Return a completed task to indicate that the operation has finished (synchronous method in async pattern).
        return Task.CompletedTask;
    }

    public Task<User> GetByIdAsync(int id)
    {
        User? userToGet = users.SingleOrDefault(p => p.Id == id);
        if (userToGet is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found");
        }
        return Task.FromResult(userToGet);
    }

    public IQueryable<User> GetMany()
    {
        return users.AsQueryable();
    }
}