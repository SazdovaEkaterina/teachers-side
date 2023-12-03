namespace TeachersSideAPI.Domain.Models;

public class Comment
{
    public Guid Id { get; set; }
    public Post Post { get; set; }
    public Teacher Creator { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime LastEdited { get; set; }
}