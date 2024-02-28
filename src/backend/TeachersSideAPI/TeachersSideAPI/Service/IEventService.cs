using TeachersSideAPI.Domain.DTO;
using TeachersSideAPI.Domain.Models;

namespace TeachersSideAPI.Service;

public interface IEventService
{
    Task<IEnumerable<Event>> GetAll();
    Task<Event?> Get(Guid id);
    Task<bool> Save(EventDto eventDto);
    Task<bool> Delete(Event evt);
}