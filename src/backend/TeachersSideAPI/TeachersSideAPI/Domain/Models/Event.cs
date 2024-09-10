using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeachersSideAPI.Domain.Models;

public class Event
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] 
    public Teacher Creator { get; set; } = new Teacher();
    
    [Required]
    [MaxLength(100, ErrorMessage = "Title length cannot exceed 100 characters")]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100, ErrorMessage = "Description length cannot exceed 100 characters")]
    public string Description { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100, ErrorMessage = "Location length cannot exceed 100 characters")]
    public string Location { get; set; } = string.Empty;
    
    [Required]
    public byte[] Image { get; set; }
    
    public DateTime DateCreated { get; set; }
    
    public DateTime LastEdited { get; set; }
    
    [Required]
    public DateTime StartDate { get; set; }
    
    [Required]
    public DateTime EndDate { get; set; }
}