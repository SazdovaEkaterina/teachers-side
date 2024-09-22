namespace TeachersSideAPI.Domain.DTO;

public class CommentDto
{
    public int Id { get; set; }

    public int PostId { get; set; } = new();
    
    public TeacherDto Creator { get; set; } = new();
    
    public string Title { get; set; } = string.Empty;
    
    public string Content { get; set; } = string.Empty;
    
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    
    public DateTime LastEdited { get; set; } = DateTime.UtcNow;
}