using TeachersSideAPI.Domain.DTO;

namespace TeachersSideAPI.Service;

public interface IPostService
{
    Task<IEnumerable<PostDto>> GetAllAsync();
    Task<PostDto?> GetAsync(int id);
    Task<bool> SaveAsync(PostDto postDto);
    Task<bool> DeleteAsync(int id);
    Task<bool> EditAsync(int id, PostDto postDto);
    Task<IEnumerable<PostDto>> GetAllByForumAsync(int forumId);
}