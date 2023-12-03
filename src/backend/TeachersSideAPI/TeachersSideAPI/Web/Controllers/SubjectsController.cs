using Microsoft.AspNetCore.Mvc;
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
}