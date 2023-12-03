using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TeachersSideAPI.Domain.Models;

namespace TeachersSideAPI.Persistence;

public class TeachersSideContext : IdentityDbContext<Teacher>
{
    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<Event> Events { get; set; } = null!;
    public DbSet<Forum> Forums { get; set; } = null!;
    public DbSet<Material> Materials { get; set; } = null!;
    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<Subject> Subjects { get; set; } = null!;
    public DbSet<Teacher> Teachers { get; set; } = null!;

    public TeachersSideContext(DbContextOptions<TeachersSideContext> options)
        : base(options)
    {
    }
}