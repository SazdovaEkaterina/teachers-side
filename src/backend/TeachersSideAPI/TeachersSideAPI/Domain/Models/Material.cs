using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeachersSideAPI.Domain.Enums;

namespace TeachersSideAPI.Domain.Models;

public class Material
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public Subject Subject { get; set; } = new Subject();

    [Required] 
    public Teacher Creator { get; set; } = new Teacher();
    
    public DateTime DateCreated { get; set; }

    [Required]
    [MaxLength(100, ErrorMessage = "FileTitle length cannot exceed 100 characters")]
    public string FileTitle { get; set; } = string.Empty;

    [MaxLength(100, ErrorMessage = "FileTitle length cannot exceed 100 characters")]
    [Required] 
    public string FilePath { get; set; } = string.Empty;
    
    [Required]
    public FileType FileType { get; set; }

    public List<Post> RelatedPosts { get; set; }
}