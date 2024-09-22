using System.Diagnostics.Eventing.Reader;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
    private readonly UserManager<Teacher> _userManager;

    public EventService(IEventRepository eventRepository, IMapper mapper, UserManager<Teacher> userManager)
    {
        _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<IEnumerable<EventDto>> GetAllAsync()
    {
        IEnumerable<Event> events = await _eventRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<EventDto>>(events);
    }

    public async Task<EventDto?> GetAsync(int id)
    {
        var evt = await _eventRepository.GetAsync(id);
        
        EventDto eventDto = _mapper.Map<EventDto>(evt);
        
        return evt != null ? _mapper.Map<EventDto>(evt) : null;
    }

    public async Task<bool> SaveAsync(EventDto eventDto)
    {
        var evt = _mapper.Map<Event>(eventDto);
        evt.Creator = await _userManager.FindByEmailAsync(eventDto.Creator.Email)
                        ?? throw new UserNotFoundException($"User with email {evt.Creator.Email} not found");
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
        
        var mappedEvent = _mapper.Map<Event>(eventDto);
        evt.Title = mappedEvent.Title;
        evt.Description = mappedEvent.Description;
        evt.Creator = await _userManager.FindByEmailAsync(mappedEvent.Creator.Email)
                        ?? throw new UserNotFoundException($"User with email {mappedEvent.Creator.Email} not found");
        evt.Location = mappedEvent.Location;
        evt.Image = mappedEvent.Image;
        evt.LastEdited = DateTime.UtcNow;
        evt.StartDate = mappedEvent.StartDate;
        evt.EndDate = mappedEvent.EndDate;

        return await _eventRepository.SaveChangesAsync();
    }
}