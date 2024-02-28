using AutoMapper;
using TeachersSideAPI.Domain;
using TeachersSideAPI.Domain.DTO;
using TeachersSideAPI.Domain.Models;
using TeachersSideAPI.Persistence.Repositories;

namespace TeachersSideAPI.Service.Implementation;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;

    public EventService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
    }

    public Task<IEnumerable<Event>> GetAll()
    {
        return _eventRepository.GetAll();
    }

    public Task<Event?> Get(Guid id)
    {
        return _eventRepository.Get(id);
    }

    public Task<bool> Save(EventDto eventDto)
    {
        var evt = _mapper.Map<Event>(eventDto);
        evt.DateCreated = DateTime.UtcNow;
        evt.LastEdited = DateTime.UtcNow;
        return _eventRepository.Save(evt);
    }

    public Task<bool> Delete(Event evt)
    {
        return _eventRepository.Delete(evt);
    }
}