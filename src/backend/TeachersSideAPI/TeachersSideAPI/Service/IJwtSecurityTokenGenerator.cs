using System.IdentityModel.Tokens.Jwt;
using TeachersSideAPI.Domain.Models;

namespace TeachersSideAPI.Service;

public interface IJwtSecurityTokenGenerator
{
    public JwtSecurityToken CreateJwtSecurityToken(Teacher user);
}