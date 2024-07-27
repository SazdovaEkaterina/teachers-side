using System.ComponentModel.DataAnnotations;
using TeachersSideAPI.Domain.Models;

namespace TeachersSideAPI.Domain.DTO;

public class PostDto
{
    public int Id { get; set; }

    [Required]
    public ForumDto Forum { get; set; } = new ForumDto();

    [Required]
    public TeacherDto Creator { get; set; } = new TeacherDto();
    
    [Required]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    public string Content { get; set; } = string.Empty;
    
    public DateTime DateCreated { get; set; }
    
    public DateTime LastEdited { get; set; }
    
    public List<Material> RelatedMaterials { get; set; } = new List<Material>();
    
    public List<Comment> Comments { get; set; } = new List<Comment>();
}