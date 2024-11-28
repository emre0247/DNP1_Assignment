namespace Entities;

public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public int Id { get; set; }
    
    public Post Post { get; set; }
    
    public Comment Comment { get; set; }
    
}