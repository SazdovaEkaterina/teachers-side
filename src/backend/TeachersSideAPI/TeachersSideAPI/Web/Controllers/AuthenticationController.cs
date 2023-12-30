using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TeachersSideAPI.Domain.Models;
using LoginModel = TeachersSideAPI.Domain.Models.LoginModel;
using RegisterModel = TeachersSideAPI.Domain.Models.RegisterModel;

namespace TeachersSideAPI.Web.Controllers;

[ApiController]
[Route("api/authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly UserManager<Teacher> _userManager;
    private readonly IConfiguration _configuration;

    public AuthenticationController(UserManager<Teacher> userManager, 
        IConfiguration configuration)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null)
        {
            return NotFound();
        }

        if (!await _userManager.CheckPasswordAsync(user, model.Password))
        {
            return Unauthorized();
        }
        
        var jwtSecurityToken = CreateJwtSecurityToken(user);

        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            expiration = jwtSecurityToken.ValidTo
        });

    }

    private JwtSecurityToken CreateJwtSecurityToken(Teacher user)
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Authentication:SecretForKey"]));
        var signingCredentials = new SigningCredentials(
            securityKey, SecurityAlgorithms.HmacSha256);

        var claimsForToken = new List<Claim>();
        claimsForToken.Add(new Claim("first_name", user.FirstName));
        claimsForToken.Add(new Claim("last_name", user.LastName));

        var jwtSecurityToken = new JwtSecurityToken(
            _configuration["Authentication:Issuer"],
            _configuration["Authentication:Audience"],
            claimsForToken,
            DateTime.UtcNow,
            DateTime.UtcNow.AddHours(1),
            signingCredentials);
        return jwtSecurityToken;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var userExists = await _userManager.FindByEmailAsync(model.Email);
        if (userExists != null)
        {
            return Conflict();
        }

        var user = new Teacher()
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            UserName = model.UserName,
            Email = model.Email,
        };
        
        var createdUser = await _userManager.CreateAsync(user, model.Password);
        if (!createdUser.Succeeded)
        {
            return BadRequest();
        }
        
        return Ok();
    }
}