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
        
        return evt != null ? _mapper.Map<EventDto>(evt) : null;
    }

    public async Task<bool> SaveAsync(EventDto eventDto)
    {
        var evt = _mapper.Map<Event>(eventDto);
        evt.Image = await GetImageBytes(eventDto.Image);
        evt.Creator = await _userManager.FindByEmailAsync(eventDto.Creator.Email)
                        ?? throw new UserNotFoundException($"User with email {evt.Creator.Email} not found");
        evt.DateCreated = DateTime.UtcNow;
        evt.LastEdited = DateTime.UtcNow;
        return await _eventRepository.SaveAsync(evt);
    }
        
    private bool IsValidFile(IFormFile file)
    {
        List<string> validFormats = new List<string>() { ".jpg", ".png", ".svg", ".jpeg" };
        var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
        return validFormats.Contains(extension);
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
        evt.Creator = await _userManager.FindByEmailAsync(eventDto.Creator.Email)
                        ?? throw new UserNotFoundException($"User with email {evt.Creator.Email} not found");
        evt.Location = eventDto.Location;
        evt.Image = await GetImageBytes(eventDto.Image);
        evt.LastEdited = DateTime.UtcNow;
        evt.StartDate = eventDto.StartDate;
        evt.EndDate = eventDto.EndDate;

        return await _eventRepository.SaveChangesAsync();
    }

    private async Task<byte[]> GetImageBytes(IFormFile image)
    {
        if (!IsValidFile(image))
        {
            throw new BadFileExtensionException("Invalid file extensions. Valid extensions are: .jpg, .png, .svg, .jpeg.");
        }

        byte[] fileBytes = null;
        using (var stream = new MemoryStream())
        {
            await image.CopyToAsync(stream);
            fileBytes = stream.ToArray();
        }

        return fileBytes;
    }
}