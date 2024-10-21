using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class UserFileRepository : IUserRepository
{
    private readonly string filePath = "users.json";


    public UserFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }
    public async Task<User> AddAsync(User user)
    {
        string userAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(userAsJson);
        int maxId = users.Count > 0 ? users.Max(x => x.Id) + 1 : 1;
        user.Id = maxId;
        users.Add(user);
        userAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, userAsJson);
        return user;
    }

    public async Task UpdateAsync(User user)
    {
        string userAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(userAsJson)!;
        User? existingUser = users.SingleOrDefault(p => p.Id == user.Id);
        if (existingUser is null)
        {
            throw new InvalidOperationException(
                $"User with ID '{user.Id}' not found");
        }
        
        users.Remove(existingUser);
        users.Add(user);
        userAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, userAsJson);
    }

    public async Task DeleteAsync(int id)
    {
        string userAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(userAsJson)!;
        User? userToDelete = users.SingleOrDefault(p => p.Id == id);
        
        if (userToDelete is null)
        {
            throw new InvalidOperationException(
                $"User with ID '{id}' not found");
        }

        users.Remove(userToDelete);
        userAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, userAsJson);
    }

    public async Task<User> GetByIdAsync(int id)
    {
        string userAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(userAsJson)!;
        User? user = users.SingleOrDefault(p => p.Id == id);

        if (user is null)
        {
            throw new InvalidOperationException(
                $"User with ID '{id}' not found");
        }
        
        userAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, userAsJson);

        return user;
    }

    public IQueryable<User> GetMany()
    {
        string userAsJson = File.ReadAllTextAsync(filePath).Result;
        List<User> users = JsonSerializer.Deserialize<List<User>>(userAsJson)!;
        return users.AsQueryable();
        
    }
}