using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeachersSideAPI.Domain.Models;

public class Post
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public Forum Forum { get; set; }
    public Teacher Creator { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime LastEdited { get; set; }
    public List<Material> RelatedMaterials { get; set; } = new List<Material>();
    public List<Comment> Comments { get; set; } = new List<Comment>();
}