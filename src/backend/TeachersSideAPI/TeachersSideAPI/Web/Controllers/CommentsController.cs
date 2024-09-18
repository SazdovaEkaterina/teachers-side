using Microsoft.AspNetCore.Mvc;
using TeachersSideAPI.Domain.DTO;
using TeachersSideAPI.Service;

namespace TeachersSideAPI.Web.Controllers;

[ApiController]
[Route("api/comments")]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentsController(ICommentService commentService)
    {
        _commentService = commentService ?? throw new ArgumentNullException(nameof(commentService));
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommentDto>>> GetAllAsync([FromQuery]int postId)
    {
        return Ok(await _commentService.GetAllForPostAsync(postId));
    }

    [HttpPost("add")]
    public async Task<ActionResult<bool>> AddAsync([FromBody] CommentDto commentDto)
    {
        try
        {
            var result = await _commentService.SaveAsync(commentDto);
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
        var result = await _commentService.DeleteAsync(id);
        return result ? Ok(result) : NotFound();
    }
    
    [HttpPost("{id}/edit")]
    public async Task<ActionResult<bool>> EditAsync([FromRoute]int id, [FromBody] CommentDto commentDto)
    { 
        var result = await _commentService.EditAsync(id, commentDto);
        return result ? Ok(result) : NotFound();
    }
}