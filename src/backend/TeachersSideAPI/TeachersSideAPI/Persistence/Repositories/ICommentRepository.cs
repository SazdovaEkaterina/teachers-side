using TeachersSideAPI.Domain.Models;

namespace TeachersSideAPI.Persistence.Repositories;

public interface ICommentRepository
{
    Task<Comment?> GetAsync(int id);
    Task<IEnumerable<Comment>> GetAllByPostIdAsync(int postId);
    Task<bool> SaveAsync(Comment comment);
    Task<bool> DeleteAsync(Comment comment);
    Task<bool> SaveChangesAsync();   
}