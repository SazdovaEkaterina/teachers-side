using TeachersSideAPI.Domain.DTO;
using TeachersSideAPI.Domain.Models;

namespace TeachersSideAPI.Service;

public interface IEventService
{
    Task<IEnumerable<EventDto>> GetAllAsync();
    Task<EventDto?> GetAsync(int id);
    Task<bool> SaveAsync(EventDto eventDto);
    Task<bool> DeleteAsync(int id);
    Task<bool> EditAsync(int id, EventDto eventDto);

}