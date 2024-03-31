using TeachersSideAPI.Domain.Models;

namespace TeachersSideAPI.Persistence.Repositories;

public interface IEventRepository
{
    Task<IEnumerable<Event>> GetAll();
    Task<Event?> Get(int id);
    Task<bool> Save(Event evt);
    Task<bool> Delete(Event evt);
}