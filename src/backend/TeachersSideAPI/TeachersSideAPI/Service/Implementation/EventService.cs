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

    public async Task<IEnumerable<EventDto>> GetAllAsync()
    {
        IEnumerable<Event> events = await _eventRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<EventDto>>(events);
    }

    public async Task<EventDto?> GetAsync(int id)
    {
        var evt = await _eventRepository.GetAsync(id);
        
        return evt != null ? _mapper.Map<EventDto>(evt) : null;
    }

    public async Task<bool> SaveAsync(EventDto eventDto)
    {
        var evt = _mapper.Map<Event>(eventDto);
        evt.DateCreated = DateTime.UtcNow;
        evt.LastEdited = DateTime.UtcNow;
        return await _eventRepository.SaveAsync(evt);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var evt = await _eventRepository.GetAsync(id);

        if (evt == null)
            return false;
        
        return await _eventRepository.DeleteAsync(evt);
    }

    public async Task<bool> EditAsync(int id, EventDto eventDto)
    {
        var evt = await _eventRepository.GetAsync(id);

        if (evt == null)
            return false;
        
        evt.Title = eventDto.Title;
        evt.Description = eventDto.Description;
        evt.Creator = evt.Creator;
        evt.LastEdited = DateTime.UtcNow;
        evt.StartDate = eventDto.StartDate;
        evt.EndDate = eventDto.EndDate;

        return await _eventRepository.SaveChangesAsync();
    }
}