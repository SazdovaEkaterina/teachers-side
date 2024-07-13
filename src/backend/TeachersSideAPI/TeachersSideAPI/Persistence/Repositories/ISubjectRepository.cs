using TeachersSideAPI.Domain.Enums;
using TeachersSideAPI.Domain.Models;

namespace TeachersSideAPI.Persistence.Repositories;

public interface ISubjectRepository
{
    Task<IEnumerable<Subject>> GetSubjectsAsync();
    Task<Subject?> GetBySubjectNameAndCategoryAsync(string name, Category category);
}