using DTOs;

namespace BlazorApp.Services;

public interface IUserService
{
    public Task<UserDTO> addUserAsync(CreateUserDTO request);
    
    public Task<UserDTO> updateUserAsync(int userId, CreateUserDTO request);
    
    public Task<UserDTO> getUserByIdAsync(int userId);
    
    public Task deleteUserAsync(int userId);
    
    public Task<List<UserDTO>> getAllUsers(string? username);
}