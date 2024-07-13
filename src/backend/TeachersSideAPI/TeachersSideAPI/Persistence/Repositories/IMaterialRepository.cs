using TeachersSideAPI.Domain.Enums;
using TeachersSideAPI.Domain.Models;

namespace TeachersSideAPI.Persistence.Repositories;

public interface IMaterialRepository
{
    Task<IEnumerable<Material>> GetAllAsync();
    Task<Material?> GetAsync(int id);
    Task<bool> SaveAsync(Material material);
    Task<bool> DeleteAsync(Material material);
    Task<IEnumerable<Material>> GetAllBySubjectNameAndCategoryAsync(string subjectName, Category category);
    Task<bool> SaveChangesAsync();   
}