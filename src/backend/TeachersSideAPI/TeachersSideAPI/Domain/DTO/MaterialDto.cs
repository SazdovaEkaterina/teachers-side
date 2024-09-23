using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeachersSideAPI.Domain.Enums;

namespace TeachersSideAPI.Domain.DTO;

public class MaterialDto
{
    public int Id { get; set; }

    public SubjectDto Subject { get; set; } = new SubjectDto();

    public TeacherDto Creator { get; set; } = new TeacherDto();
    
    public DateTime DateCreated { get; set; }
    
    public string FileTitle { get; set; }
    
    public IFormFile? File { get; set; }

    public string? FilePath { get; set; }
    
    public FileType FileType { get; set; }
}