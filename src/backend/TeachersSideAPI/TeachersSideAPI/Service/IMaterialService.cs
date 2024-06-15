using TeachersSideAPI.Domain.DTO;

namespace TeachersSideAPI.Service;

public interface IMaterialService
{
    Task<IEnumerable<MaterialDto>> GetAllAsync();
    Task<MaterialDto?> GetAsync(int id);
    Task<bool> SaveAsync(MaterialDto materialDto);
    Task<bool> DeleteAsync(int id);
    Task<bool> EditAsync(int id, MaterialDto materialDto);
    Task<IEnumerable<MaterialDto>> GetAllBySubjectAndCategoryAsync(string subjectName, string category);

}