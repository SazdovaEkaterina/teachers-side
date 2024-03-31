using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachersSideAPI.Domain.DTO;
using TeachersSideAPI.Domain.Models;
using TeachersSideAPI.Service;
using TeachersSideAPI.Service.Exceptions;

namespace TeachersSideAPI.Web.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
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
    public async Task<ActionResult<IEnumerable<Event>>> GetAll()
    {
        return Ok(await _eventService.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Event>> Get([FromRoute]int id)
    {
        try
        {
            var evt = await _eventService.Get(id);
            return Ok(evt);
        }
        catch (EventNotFoundException exception)
        {
            return NotFound(exception.Message);
        }
    }

    [HttpPost("add")]
    public async Task<ActionResult<bool>> Add([FromBody] EventDto eventDto)
    {
        var result = await _eventService.Save(eventDto);
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete([FromRoute] int id)
    {
        try
        {
            var result = await _eventService.Delete(id);
            return Ok(result);
        }
        catch (EventNotFoundException exception)
        {
            return NotFound(exception.Message);
        }
    }
}