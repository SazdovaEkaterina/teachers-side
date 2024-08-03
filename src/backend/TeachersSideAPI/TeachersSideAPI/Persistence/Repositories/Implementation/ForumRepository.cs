using Microsoft.EntityFrameworkCore;
using TeachersSideAPI.Domain.Models;

namespace TeachersSideAPI.Persistence.Repositories.Implementation;

public class ForumRepository : IForumRepository
{
    private readonly TeachersSideContext _context;

    public ForumRepository(TeachersSideContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public async Task<Forum?> GetAsync(int id)
    {
        return await _context.Forums
            .Include(x => x.Subject)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
    
    public async Task<IEnumerable<Forum>> GetAllAsync()
    {
        return await _context.Forums
            .Include(x => x.Subject)
            .ToListAsync();
    }
    
    public async Task<bool> SaveAsync(Forum forum)
    {
        await _context.Forums.AddAsync(forum);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Forum forum)
    {
        _context.Forums.Remove(forum);
        return await _context.SaveChangesAsync() > 0;
    }
    
    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
    
}