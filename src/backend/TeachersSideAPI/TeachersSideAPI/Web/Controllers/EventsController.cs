using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachersSideAPI.Domain.DTO;
using TeachersSideAPI.Domain.Models;
using TeachersSideAPI.Service;

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
    public async Task<ActionResult<Event>> Get([FromRoute]Guid id)
    {
        var evt = await _eventService.Get(id);
        return evt != null ? Ok(evt) : NotFound();
    }

    [HttpPost("add")]
    public async Task<ActionResult<bool>> Add([FromBody] EventDto eventDto)
    {
        var result = await _eventService.Save(eventDto);
        return Ok(result);
    }
}