using Microsoft.EntityFrameworkCore;
using TeachersSideAPI.Domain.Models;

namespace TeachersSideAPI.Persistence.Repositories.Implementation;

public class PostRepository : IPostRepository
{
    private readonly TeachersSideContext _context;

    public PostRepository(TeachersSideContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public async Task<IEnumerable<Post>> GetAllAsync()
    {
        return await _context.Posts
            .Include(x => x.Forum)
            .Include(x => x.Creator)
            .ToListAsync();
    }

    public async Task<Post?> GetAsync(int id)
    {
        return await _context.Posts
            .Include(x => x.Forum)
            .Include(x => x.Creator)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> SaveAsync(Post post)
    {
        await _context.Posts.AddAsync(post);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Post post)
    {
        _context.Posts.Remove(post);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<Post>> GetAllByForumAsync(Forum forum)
    {
        return await _context.Posts
            .Include(x => x.Forum)
            .Include(x => x.Creator)
            .Where(x => x.Forum == forum)
            .ToListAsync();
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}