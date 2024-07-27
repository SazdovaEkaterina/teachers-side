using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachersSideAPI.Domain.DTO;
using TeachersSideAPI.Service;
using TeachersSideAPI.Service.Exceptions;

namespace TeachersSideAPI.Web.Controllers;

[ApiController]
[Route("api/posts")]
public class PostsController : ControllerBase
{
    private readonly IPostService _postService;

    public PostsController(IPostService postService)
    {
        _postService = postService ?? throw new ArgumentNullException(nameof(postService));
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostDto>>> GetAllAsync()
    {
        return Ok(await _postService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MaterialDto>> GetAsync([FromRoute]int id)
    {
        var postDto = await _postService.GetAsync(id);
        return postDto != null ? Ok(postDto) : NotFound();
    }

    [HttpPost("add")]
    public async Task<ActionResult<bool>> AddAsync([FromBody] PostDto postDto)
    {
        try
        {
            var result = await _postService.SaveAsync(postDto);
            return Ok(result);
        }
        catch (Exception exception)
        {
            return Conflict();
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteAsync([FromRoute] int id)
    {
        var result = await _postService.DeleteAsync(id);
        return result ? Ok(result) : NotFound();
    }
    
    [HttpPost("{id}/edit")]
    public async Task<ActionResult<bool>> EditAsync([FromRoute]int id, [FromBody] PostDto postDto)
    { 
        var result = await _postService.EditAsync(id, postDto);
        return result ? Ok(result) : NotFound();
    }
}