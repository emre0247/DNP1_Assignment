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
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await userRepository.FindByUserNameAsync(request.Username);

        if (user == null)
        {
            return Unauthorized("User not found");
        }

        // Valider brugerens password (forudsat at det også er en del af request)
        if (user.Password != request.Password)
        {
            return Unauthorized("Invalid password");
        }
        // Konverter brugeren til en DTO (uden følsomme oplysninger) og returner den
        var userDto = new UserDTO
        {
            Id = user.Id,
            Username = user.Username,
            // Andre nødvendige felter, men uden password
        };

        return Ok(userDto);
    }
}