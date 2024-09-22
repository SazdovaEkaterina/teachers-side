using TeachersSideAPI.Domain.Models;

namespace TeachersSideAPI.Domain.DTO;

public class EventDto
{
    public int Id { get; set; }
    
    public TeacherDto Creator { get; set; } = new TeacherDto();
    
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public string Location { get; set; } = string.Empty;

    public IFormFile? Image { get; set; }
    
    public string? ImagePath { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
}