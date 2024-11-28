namespace Entities;

public class Comment
{
    public string Body { get; set; }
    public int Id { get; set; }
    public int PostId { get; set; }
    public int UserId { get; set; }
    
    public Post Post { get; set; } // Navigation Property
    
    public User User { get; set; } // Navigation property
    

    

  
   
}