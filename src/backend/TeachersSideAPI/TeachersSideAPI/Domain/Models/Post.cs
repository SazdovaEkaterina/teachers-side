using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeachersSideAPI.Domain.Models;

public class Post
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] 
    public Forum Forum { get; set; } = new Forum();

    [Required] 
    public Teacher Creator { get; set; } = new Teacher();

    [Required]
    [MaxLength(100, ErrorMessage = "Title length cannot exceed 100 characters")]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    public string Content { get; set; } = string.Empty;
    
    [Required]
    public DateTime DateCreated { get; set; }
    
    public DateTime LastEdited { get; set; }
    
    public List<Material> RelatedMaterials { get; set; } = new List<Material>();
    
    public List<Comment> Comments { get; set; } = new List<Comment>();
}