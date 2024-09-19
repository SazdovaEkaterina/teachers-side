using Microsoft.EntityFrameworkCore;
using TeachersSideAPI.Domain.Models;

namespace TeachersSideAPI.Persistence.Repositories.Implementation;

public class CommentRepository : ICommentRepository
{
    private readonly TeachersSideContext _context;

    public CommentRepository(TeachersSideContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Comment?> GetAsync(int id)
    {
        return await _context.Comments
            .Include(comment => comment.Post)
            .Include(comment => comment.Creator)
            .Where(comment => comment.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Comment>> GetAllByPostIdAsync(int postId)
    {
        return await _context.Comments
            .Include(comment => comment.Post)
            .Include(comment => comment.Creator)
            .Where(comment => comment.Post.Id == postId)
            .ToListAsync();
    }

    public async Task<bool> SaveAsync(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Comment comment)
    {
        _context.Comments.Remove(comment);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}