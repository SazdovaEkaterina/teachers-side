using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeachersSideAPI.Domain.Models;

namespace TeachersSideAPI.Web.Controllers;

[ApiController]
[Route("api/authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly UserManager<Teacher> _userManager;

    public AuthenticationController(UserManager<Teacher> userManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }
}