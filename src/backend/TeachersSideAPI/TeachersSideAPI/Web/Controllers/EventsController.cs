using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TeachersSideAPI.Domain.DTO;
using TeachersSideAPI.Domain.Models;
using TeachersSideAPI.Service;
using TeachersSideAPI.Service.Exceptions;

namespace TeachersSideAPI.Web.Controllers;

[ApiController]
[Route("api/events")]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;
    private readonly IMapper _mapper;

    public EventsController(IEventService eventService, IMapper mapper)
    {
        _eventService = eventService ?? throw new ArgumentNullException(nameof(eventService));
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EventDto>>> GetAllAsync()
    {
        return Ok(await _eventService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EventDto>> Get([FromRoute]int id)
    {
        var evt = await _eventService.GetAsync(id);
        return evt != null ? Ok(evt) : NotFound();
    }

    [HttpPost("add")]
    public async Task<ActionResult<bool>> AddAsync([FromForm] EventDto eventDto)
    {
        try
        {
            if (Request.Form.TryGetValue("creatorDto", out var creatorJson))
            {
                var creator = JsonConvert.DeserializeObject<TeacherDto>(creatorJson);
                eventDto.Creator = creator; 
            }
            
            var result = await _eventService.SaveAsync(eventDto);
            return Ok(result);
        }
        catch (UserNotFoundException exception)
        {
            return Conflict();
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteAsync([FromRoute] int id)
    {
        var evt = await _eventService.GetAsync(id);
        if (evt == null)
        {
            return NotFound();
        }
        var result = await _eventService.DeleteAsync(id);
        return Ok(result);
    }
    
    [HttpPost("{id}/edit")]
    public async Task<ActionResult<bool>> EditAsync([FromRoute]int id, [FromForm] EventDto eventDto)
    { 
        try
        {
            if (Request.Form.TryGetValue("creatorDto", out var creatorJson))
            {
                var creator = JsonConvert.DeserializeObject<TeacherDto>(creatorJson);
                eventDto.Creator = creator; 
            }
            
            var result = await _eventService.EditAsync(id, eventDto);
            return Ok(result);
        }
        catch (UserNotFoundException exception)
        {
            return Conflict();
        }
    }
}