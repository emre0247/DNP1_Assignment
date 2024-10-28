namespace DTOs;

public class CreateCommentDTO
{
    public int PostId { get; set; }
    public int UserId { get; set; }
    public string Body { get; set; }
}