using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachersSideAPI.Domain.Models;
using TeachersSideAPI.Service;

namespace TeachersSideAPI.Web.Controllers;

[ApiController]
[Route("api/subjects")]
public class SubjectsController : ControllerBase
{
    private readonly ISubjectService _subjectService;

    public SubjectsController(ISubjectService subjectService)
    {
        _subjectService = subjectService ?? throw new ArgumentNullException(nameof(subjectService));
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Subject>>> GetAll()
    {
        var subjects = await _subjectService.GetSubjectsAsync();
        return Ok(subjects);
    }
}