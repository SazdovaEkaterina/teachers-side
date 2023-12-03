using TeachersSideAPI.Domain.Enums;

namespace TeachersSideAPI.Domain.Models;

public class Material
{
    public Guid Id { get; set; }
    public Subject Subject { get; set; }
    public Teacher Creator { get; set; }
    public DateTime DateCreated { get; set; }
    public string FileTitle { get; set; }
    public string FilePath { get; set; }
    public FileType FileType { get; set; }
    public List<Post> RelatedPosts { get; set; }
}