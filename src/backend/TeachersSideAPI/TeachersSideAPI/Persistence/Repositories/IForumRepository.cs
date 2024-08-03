using TeachersSideAPI.Domain.Models;

namespace TeachersSideAPI.Persistence.Repositories;

public interface IForumRepository
{
    Task<IEnumerable<Forum>> GetAllAsync();
    Task<Forum?> GetAsync(int id);
    Task<bool> SaveAsync(Forum forum);
    Task<bool> DeleteAsync(Forum forum);
    Task<bool> SaveChangesAsync();}