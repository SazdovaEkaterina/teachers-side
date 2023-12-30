using TeachersSideAPI.Domain.Models;

namespace TeachersSideAPI.Service;

public interface ISubjectService
{
    Task<IEnumerable<Subject>> GetSubjectsAsync();
}