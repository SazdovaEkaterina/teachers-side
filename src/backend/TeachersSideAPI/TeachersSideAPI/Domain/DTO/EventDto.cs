using TeachersSideAPI.Domain.Models;

namespace TeachersSideAPI.Domain.DTO;

public class EventDto
{
    public TeacherDto Creator { get; set; } = new TeacherDto();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}