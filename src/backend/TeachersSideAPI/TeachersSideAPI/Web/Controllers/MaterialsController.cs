using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachersSideAPI.Domain.DTO;
using TeachersSideAPI.Service;
using TeachersSideAPI.Service.Exceptions;

namespace TeachersSideAPI.Web.Controllers;

[ApiController]
[Route("api/materials")]
public class MaterialsController : ControllerBase
{
    private readonly IMaterialService _materialService;

    public MaterialsController(IMaterialService materialService)
    {
        _materialService = materialService ?? throw new ArgumentNullException(nameof(materialService));
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MaterialDto>>> GetAllAsync()
    {
        return Ok(await _materialService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MaterialDto>> GetAsync([FromRoute]int id)
    {
        var evt = await _materialService.GetAsync(id);
        return evt != null ? Ok(evt) : NotFound();
    }

    [HttpPost("add")]
    public async Task<ActionResult<bool>> AddAsync([FromBody] MaterialDto materialDto)
    {
        try
        {
            var result = await _materialService.SaveAsync(materialDto);
            return Ok(result);
        }
        catch (Exception exception)
        {
            return Conflict(exception.Message);
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteAsync([FromRoute] int id)
    {
        var result = await _materialService.DeleteAsync(id);
        return result ? Ok(result) : NotFound();
    }
    
    [HttpPost("{id}/edit")]
    public async Task<ActionResult<bool>> EditAsync([FromRoute]int id, [FromBody] MaterialDto materialDto)
    { 
        var result = await _materialService.EditAsync(id, materialDto);
        return result ? Ok(result) : NotFound();
    }
    
    [HttpGet("{subjectCategory}/{subjectName}")]
    public async Task<ActionResult<IEnumerable<MaterialDto>>> GetAllBySubjectAsync([FromRoute] string subjectCategory, [FromRoute] string subjectName)
    {
        return Ok(await _materialService.GetAllBySubjectAndCategoryAsync(subjectName, subjectCategory));
    }
}