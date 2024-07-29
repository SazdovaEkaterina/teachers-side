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
    
    public async Task<Forum?> FindByIdAsync(int id)
    {
        return await _context.Forums
            .Include(x => x.Subject)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}