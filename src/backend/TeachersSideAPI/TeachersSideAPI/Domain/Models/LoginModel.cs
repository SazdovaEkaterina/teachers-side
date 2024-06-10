using System.ComponentModel.DataAnnotations;

namespace TeachersSideAPI.Domain.Models;

public class LoginModel
{
    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    [MaxLength(25, ErrorMessage = "Email length cannot exceed 25 characters")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [MaxLength(25, ErrorMessage = "Password length cannot exceed 25 characters")]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{8,15}$",
        ErrorMessage = "Password is not valid")]
    public string Password { get; set; } = string.Empty;
}