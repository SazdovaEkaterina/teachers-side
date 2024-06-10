using TeachersSideAPI.Domain.Models;

namespace TeachersSideAPI.Persistence.Repositories;

public interface IEventRepository
{
    Task<IEnumerable<Event>> GetAllAsync();
    Task<Event?> GetAsync(int id);
    Task<bool> SaveAsync(Event evt);
    Task<bool> DeleteAsync(Event evt);
    Task<bool> SaveChangesAsync();
}