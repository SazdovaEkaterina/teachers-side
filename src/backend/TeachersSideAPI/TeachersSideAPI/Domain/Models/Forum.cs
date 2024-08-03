using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeachersSideAPI.Domain.Models;

public class Forum
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public Subject? Subject { get; set; }
    
    public List<Post> Posts { get; set; } = new List<Post>();
}