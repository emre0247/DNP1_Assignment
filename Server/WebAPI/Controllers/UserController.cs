using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;


namespace WebAPI.Controllers;

[ApiController]
[Route("users")]
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
            await _userRepository.VerifyUserNameIsAvailableAsync(request.Username);
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
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
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
            return StatusCode(500, e.Message);
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
            return NotFound(new { message = e.Message });
        }
        catch (Exception e)
        {
           return StatusCode(500, new { message = $"An unexpected error occurred: {e.Message}" });
        }
    }

    // Method to update a user based on Id
    [HttpPut("{id:int}")] 
    public async Task<IActionResult> UpdateUser([FromBody] CreateUserDTO request, int id)
    {
        try
        {
            var existingUser = await _userRepository.GetByIdAsync(id);

            if (existingUser == null)
            {
                return NotFound("User not found");
            }

            existingUser.Username = request.Username;
            existingUser.Password = request.Password;

            await _userRepository.UpdateAsync(existingUser);

            UserDTO dto = new UserDTO
            {
                Id = existingUser.Id,
                Username = existingUser.Username,
            };

            return Ok(dto);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
        }
    }


    
    // Method to get users based on an optional username filter
    [HttpGet]
    public async Task<ActionResult<List<UserDTO>>> GetUsers([FromQuery] string? username)
    {
        try
        {
            var users = _userRepository.GetMany();

            if (!string.IsNullOrEmpty(username))
            {
                users = users.Where(u => u.Username.Contains(username, StringComparison.OrdinalIgnoreCase));
            }

            var userDto = users.Select(user => new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
            }).ToList();

            return Ok(userDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving users: {ex.Message}");
        }
    }

        
    

    
    
}