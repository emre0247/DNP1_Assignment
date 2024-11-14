using DTOs;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository userRepository;
    public AuthController(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    [HttpPost("login")]
    public async Task<ActionResult<UserDTO>> Login([FromQuery] string username, [FromQuery] string password)
    {
        try
        {
            var user = await userRepository.FindByUserNameAsync(username);
            if (user == null)
            {
                return Unauthorized("Invalid username");
            }

            if (user.Password != password)
            {
                return Unauthorized("Invalid password");
            }

            UserDTO userDto = new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
            };
            return Ok(userDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        
    }
}