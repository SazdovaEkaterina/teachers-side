using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeachersSideAPI.Domain.Enums;

namespace TeachersSideAPI.Domain.Models;

public class Subject
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Category Category { get; set; }
    public List<Teacher> Teachers { get; set; }
}