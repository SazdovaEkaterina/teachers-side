using TeachersSideAPI.Domain.Models;

namespace TeachersSideAPI.Domain.DTO;

public class EventDto
{
    public Guid CreatorId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}