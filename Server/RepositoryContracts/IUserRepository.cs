using Entities;

namespace RepositoryContracts;

public interface IUserRepository
{
    Task<User> AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(int id);
    Task<User> GetByIdAsync(int id);
    IQueryable<User> GetMany();
    Task<User?> FindByUserNameAsync(string userName);
    Task VerifyUserNameIsAvailableAsync(string userName);
}