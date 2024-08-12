using TeachersSideAPI.Domain.DTO;
using TeachersSideAPI.Service.Exceptions;

namespace TeachersSideAPI.Service;

public interface IForumService
{
    Task<IEnumerable<ForumDto>> GetAllAsync();
    Task<ForumDto?> GetAsync(int id);
    Task<bool> SaveAsync(ForumDto forumDto);
    Task<bool> DeleteAsync(int id);
    Task<bool> EditAsync(int id, ForumDto forum);
}