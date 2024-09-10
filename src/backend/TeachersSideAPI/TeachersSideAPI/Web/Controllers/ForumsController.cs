using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachersSideAPI.Domain.DTO;
using TeachersSideAPI.Persistence.Repositories;
using TeachersSideAPI.Service;
using TeachersSideAPI.Service.Exceptions;

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
    
    [HttpGet("{id}/posts")]
    public async Task<ActionResult<PostDto>> GetByPostsByForumAsync([FromRoute]int id)
    {
        return Ok(await _postService.GetAllPostsByForumAsync(id));
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ForumDto>>> GetAllAsync()
    {
        return Ok(await _forumService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ForumDto>> Get([FromRoute]int id)
    {
        var forumDto = await _forumService.GetAsync(id);
        return forumDto != null ? Ok(forumDto) : NotFound();
    }

    [HttpPost("add")]
    public async Task<ActionResult<bool>> AddAsync([FromBody] ForumDto forumDto)
    {
        try
        {
            var result = await _forumService.SaveAsync(forumDto);
            return Ok(result);
        }
        catch (SubjectNotFoundException exception)
        {
            return Conflict();
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteAsync([FromRoute] int id)
    {
        var result = await _forumService.DeleteAsync(id);
        return result ? Ok(result) : NotFound();
    }
    
    [HttpPost("{id}/edit")]
    public async Task<ActionResult<bool>> EditAsync([FromRoute]int id, [FromBody] ForumDto forumDto)
    { 
        var result = await _forumService.EditAsync(id, forumDto);
        return result ? Ok(result) : NotFound();
    }
}