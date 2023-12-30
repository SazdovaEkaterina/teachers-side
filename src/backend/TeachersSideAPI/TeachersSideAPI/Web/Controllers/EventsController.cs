using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachersSideAPI.Service;

namespace TeachersSideAPI.Web.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
[Route("api/events")]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventsController(IEventService eventService)
    {
        _eventService = eventService ?? throw new ArgumentNullException(nameof(eventService));
    }
}