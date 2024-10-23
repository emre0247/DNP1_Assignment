using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;


namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        this._userRepository = userRepository;
    }
    
    // Method to create a user
    [HttpPost]
    public async Task<ActionResult<UserDTO>> AddUser([FromBody] CreateUserDTO request)
    {
        try
        {
            await VerifyUserNameIsAvailableAsync(request.Username);
            User user = new User
            {
                Username = request.Username,
                Password = request.Password,
            };
        
            User created = await _userRepository.AddAsync(user);
            UserDTO dto = new()
            {
                Id = created.Id,
                Username = created.Username
            };
            
            return Created($"/Users/{dto.Id}", created);
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e);
            return StatusCode(400, e.Message);
        }
        
        

    }
    
    
    private async Task VerifyUserNameIsAvailableAsync(string userName)
    {
        var existingUser = await _userRepository.FindByUserNameAsync(userName);
        if (existingUser != null)
        {
            throw new InvalidOperationException("Username is already taken.");
        }
    }

    
    
}