using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeachersSideAPI.Domain.Enums;

namespace TeachersSideAPI.Domain.DTO;

public class SubjectDto
{
    public int Id { get; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public Category Category { get; set; }
    
    public List<TeacherDto>? Teachers { get; set; }
}