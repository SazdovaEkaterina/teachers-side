using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TeachersSideAPI.Domain.Models;

namespace TeachersSideAPI.Service.Implementation;

public class JwtSecurityTokenGenerator : IJwtSecurityTokenGenerator
{
    private readonly IConfiguration _configuration;

    public JwtSecurityTokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public JwtSecurityToken CreateJwtSecurityToken(Teacher user)
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Authentication:SecretForKey"]));
        var signingCredentials = new SigningCredentials(
            securityKey, SecurityAlgorithms.HmacSha256);

        var claimsForToken = new List<Claim>();
        claimsForToken.Add(new Claim("first_name", user.FirstName));
        claimsForToken.Add(new Claim("last_name", user.LastName));
        claimsForToken.Add(new Claim("email", user.Email));

        var jwtSecurityToken = new JwtSecurityToken(
            _configuration["Authentication:Issuer"],
            _configuration["Authentication:Audience"],
            claimsForToken,
            DateTime.UtcNow,
            DateTime.UtcNow.AddHours(1),
            signingCredentials);
        return jwtSecurityToken;
    }
}