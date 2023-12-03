using TeachersSideAPI.Domain.Enums;

namespace TeachersSideAPI.Domain.Models;

public class Subject
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Category Category { get; set; }
    public List<Teacher> Teachers { get; set; }
}