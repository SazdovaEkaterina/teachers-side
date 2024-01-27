using TeachersSideAPI.Domain.Models;

namespace TeachersSideAPI.Persistence.Repositories;

public interface ISubjectRepository
{
    Task<IEnumerable<Subject>> GetSubjectsAsync();
}