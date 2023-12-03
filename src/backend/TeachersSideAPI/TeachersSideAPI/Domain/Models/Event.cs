namespace TeachersSideAPI.Domain.Models;

public class Event
{
    public Guid Id { get; set; }
    public Teacher Creator { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime LastEdited { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}