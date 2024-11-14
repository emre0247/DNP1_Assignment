namespace DTOs;

public class LoginRequest
{
    public LoginRequest(string userName, string password)
    {
        Username = userName;
        Password = password;
    }

    public string Username { get; set; }
    public string Password { get; set; }

  
}