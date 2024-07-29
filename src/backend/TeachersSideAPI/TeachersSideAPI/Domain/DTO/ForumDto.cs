using TeachersSideAPI.Domain.Models;

namespace TeachersSideAPI.Domain.DTO;

public class ForumDto
{
    public int Id { get; set; }
    
    public Subject? Subject { get; set; }
    
    public List<PostDto> Posts { get; set; } = new List<PostDto>();
}