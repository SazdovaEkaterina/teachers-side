using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeachersSideAPI.Domain.Enums;

namespace TeachersSideAPI.Domain.DTO;

public class MaterialDto
{
    public int Id { get; }
    
    [Required]
    public SubjectDto Subject { get; set; }
    
    [Required]
    public TeacherDto Creator { get; set; }
    
    public DateTime DateCreated { get; set; }
    
    [Required]
    public string FileTitle { get; set; }
    
    [Required]
    public string FilePath { get; set; }
    
    [Required]
    public FileType FileType { get; set; }
}