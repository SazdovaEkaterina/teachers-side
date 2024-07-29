using TeachersSideAPI.Domain.Models;

namespace TeachersSideAPI.Persistence.Repositories;

public interface IPostRepository
{
    Task<IEnumerable<Post>> GetAllAsync();
    Task<Post?> GetAsync(int id);
    Task<bool> SaveAsync(Post post);
    Task<bool> DeleteAsync(Post post);
    Task<IEnumerable<Post>> GetAllByForumAsync(Forum forum);
    Task<bool> SaveChangesAsync();   
}