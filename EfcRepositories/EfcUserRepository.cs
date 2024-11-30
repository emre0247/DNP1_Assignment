using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepositories;

public class EfcUserRepository : IUserRepository
{
    private readonly AppContext ctx;

    public EfcUserRepository(AppContext ctx)
    {
        this.ctx = ctx;
    }
    public async Task<User> AddAsync(User user)
    {
        EntityEntry<User> entry = ctx.Users.Add(user);
        await ctx.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task UpdateAsync(User user)
    {
        if (!(await ctx.Users.AnyAsync(u => u.Id == user.Id)))
        {
            throw new InvalidOperationException($"User with ID '{user.Id}' not found");
        }
        ctx.Users.Update(user);
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        User? existing = await ctx.Users.SingleOrDefaultAsync(u => u.Id == id);
        if (existing == null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found");
        }
        ctx.Users.Remove(existing);
        await ctx.SaveChangesAsync();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        User? existing = await ctx.Users.SingleOrDefaultAsync(u => u.Id == id);
        if (existing == null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found");
        }
        return existing;
    }

    public IQueryable<User> GetMany()
    {
        return ctx.Users.AsQueryable();
    }

    public async Task<User?> FindByUserNameAsync(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
        {
            throw new ArgumentException("Username cannot be empty or whitespace.", nameof(userName));
        }

        return await ctx.Users.SingleOrDefaultAsync(u => u.Username == userName);
    }

    public async Task VerifyUserNameIsAvailableAsync(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
        {
            throw new ArgumentException("Username cannot be empty or whitespace.", nameof(userName));
        }

        bool isTaken = await ctx.Users.AnyAsync(u => u.Username == userName);
        if (isTaken)
        {
            throw new InvalidOperationException($"Username \"{userName}\" is already taken.");
        }
    }
}