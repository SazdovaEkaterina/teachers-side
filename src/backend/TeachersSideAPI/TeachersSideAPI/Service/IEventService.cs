using TeachersSideAPI.Domain.DTO;
using TeachersSideAPI.Domain.Models;

namespace TeachersSideAPI.Service;

public interface IEventService
{
    Task<IEnumerable<EventDto>> GetAll();
    Task<EventDto?> Get(int id);
    Task<bool> Save(EventDto eventDto);
    Task<bool> Delete(int id);
}