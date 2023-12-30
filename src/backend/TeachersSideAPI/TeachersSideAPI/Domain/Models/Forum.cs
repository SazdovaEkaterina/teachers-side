namespace TeachersSideAPI.Domain.Models;

public class Forum
{
    public Guid Id { get; set; }
    public Subject? Subject { get; set; }
    public List<Post> Posts { get; set; } = new List<Post>();
}