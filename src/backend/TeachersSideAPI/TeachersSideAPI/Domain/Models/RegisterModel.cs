using System.ComponentModel.DataAnnotations;

namespace TeachersSideAPI.Domain.Models;

public class RegisterModel
{
    [Required(ErrorMessage = "First Name is required")]
    [MaxLength(25, ErrorMessage = "First Name length cannot exceed 25 characters")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last Name is required")]
    [MaxLength(25, ErrorMessage = "Last Name length cannot exceed 25 characters")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "User Name is required")]
    [RegularExpression("^(?=[a-zA-Z0-9._]{8,20}$)(?!.*[_.]{2})[^_.].*[^_.]$", ErrorMessage = "Username is not valid")]
    public string UserName { get; set; } = string.Empty;
  
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