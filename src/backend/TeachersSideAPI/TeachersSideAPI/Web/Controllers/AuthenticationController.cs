using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeachersSideAPI.Domain.Models;
using TeachersSideAPI.Service;
using LoginModel = TeachersSideAPI.Domain.Models.LoginModel;
using RegisterModel = TeachersSideAPI.Domain.Models.RegisterModel;

namespace TeachersSideAPI.Web.Controllers;

[ApiController]
[Route("api/authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly UserManager<Teacher> _userManager;
    private readonly IJwtSecurityTokenGenerator _jwtSecurityTokenGenerator;

    public AuthenticationController(UserManager<Teacher> userManager,
        IJwtSecurityTokenGenerator jwtSecurityTokenGenerator)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _jwtSecurityTokenGenerator = jwtSecurityTokenGenerator;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("login")]
    public async Task<ActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null) return NotFound();

        var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, model.Password);
        if (!isPasswordCorrect) return Unauthorized();

        var jwtSecurityToken = _jwtSecurityTokenGenerator.CreateJwtSecurityToken(user);

        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            expiration = jwtSecurityToken.ValidTo
        });
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("register")]
    public async Task<ActionResult> Register([FromBody] RegisterModel model)
    {
        var existingUser = await _userManager.FindByEmailAsync(model.Email);
        if (existingUser != null) return Conflict("Email already in use.");
        
        existingUser = await _userManager.FindByNameAsync(model.UserName);
        if (existingUser != null) return Conflict("Username already in use.");

        var user = new Teacher()
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            UserName = model.UserName,
            Email = model.Email
        };

        var createdUser = await _userManager.CreateAsync(user, model.Password);
        if (!createdUser.Succeeded) return BadRequest(createdUser.Errors);

        return Ok();
    }
}