using System.Diagnostics.Eventing.Reader;
using AutoMapper;
using TeachersSideAPI.Domain;
using TeachersSideAPI.Domain.DTO;
using TeachersSideAPI.Domain.Models;
using TeachersSideAPI.Persistence.Repositories;
using TeachersSideAPI.Service.Exceptions;

namespace TeachersSideAPI.Service.Implementation;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;

    public EventService(IEventRepository eventRepository, IMapper mapper)
    {
        _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
        _mapper = mapper;
    }

    public async Task<IEnumerable<EventDto>> GetAll()
    {
        IEnumerable<Event> events = await _eventRepository.GetAll();
        return _mapper.Map<IEnumerable<EventDto>>(events);
    }

    public async Task<EventDto?> Get(int id)
    {
        var evt = await _eventRepository.Get(id)
            ?? throw new EventNotFoundException($"Event with ID:{id} not found.");
        return _mapper.Map<EventDto>(evt);
    }

    public async Task<bool> Save(EventDto eventDto)
    {
        var evt = _mapper.Map<Event>(eventDto);
        evt.DateCreated = DateTime.UtcNow;
        evt.LastEdited = DateTime.UtcNow;
        return await _eventRepository.Save(evt);
    }

    public async Task<bool> Delete(int id)
    {
        var evt = await _eventRepository.Get(id)
                  ?? throw new EventNotFoundException($"Event with ID:{id} not found.");
        
        return await _eventRepository.Delete(evt);
    }
}