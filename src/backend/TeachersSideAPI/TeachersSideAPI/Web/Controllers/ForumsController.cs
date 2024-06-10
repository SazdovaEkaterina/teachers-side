using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachersSideAPI.Service;

namespace TeachersSideAPI.Web.Controllers;

[ApiController]
[Route("api/forums")]
public class ForumsController : ControllerBase
{
    private readonly IForumService _forumService;

    public ForumsController(IForumService forumService)
    {
        _forumService = forumService ?? throw new ArgumentNullException(nameof(forumService));
    }
}