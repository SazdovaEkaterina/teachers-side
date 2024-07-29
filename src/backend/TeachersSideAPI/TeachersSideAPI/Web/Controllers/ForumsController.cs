using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachersSideAPI.Domain.DTO;
using TeachersSideAPI.Persistence.Repositories;
using TeachersSideAPI.Service;

namespace TeachersSideAPI.Web.Controllers;

[ApiController]
[Route("api/forums")]
public class ForumsController : ControllerBase
{
    private readonly IForumService _forumService;
    private readonly IPostService _postService;

    public ForumsController(IForumService forumService, IPostService postService)
    {
        _forumService = forumService ?? throw new ArgumentNullException(nameof(forumService));
        _postService = postService ?? throw new ArgumentNullException(nameof(postService));
    }
    
    [HttpGet("/{id}/posts")]
    public async Task<ActionResult<PostDto>> GetByForum([FromRoute]int id)
    {
        return Ok(await _postService.GetAllByForumAsync(id));
    }
}