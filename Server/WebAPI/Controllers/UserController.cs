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
            
            return Created($"/Users/{dto.Id}", dto);
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e);
            return BadRequest(new{message = e.Message});
        }
    }
    
    // Method to get specific User with id
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserDTO>> GetUserById(int id)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(id);
       

            UserDTO dto = new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
            };
            return Ok(dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound("User not found");
        }
        
    }
    
    // Method to delete user based on Id
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<UserDTO>> DeleteUser(int id)
    {
        try
        {
            await _userRepository.DeleteAsync(id);
            return Ok("User deleted");
            
        }
        catch (InvalidOperationException e)
        {
           return NotFound(new{message = e.Message});
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateUser([FromBody] CreateUserDTO request, int id)
    {
        var existingUser = await _userRepository.GetByIdAsync(id);

        
        if (existingUser == null)
        {
            return NotFound();
        }
        
        existingUser.Username = request.Username;
        existingUser.Password = request.Password;
        await _userRepository.UpdateAsync(existingUser);

        UserDTO dto = new UserDTO
        {
            Id = existingUser.Id,
            Username = existingUser.Username,
        };
        
        return dto is not null ? Ok(dto) : BadRequest("User not found");
        
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