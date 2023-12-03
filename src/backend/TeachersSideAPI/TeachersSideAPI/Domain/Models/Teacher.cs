using Microsoft.AspNetCore.Identity;

namespace TeachersSideAPI.Domain.Models;

public class Teacher : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public List<Subject> Subjects { get; set; } = new List<Subject>();
}