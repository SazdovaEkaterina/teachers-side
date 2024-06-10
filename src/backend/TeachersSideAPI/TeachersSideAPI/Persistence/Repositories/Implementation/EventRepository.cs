using Microsoft.EntityFrameworkCore;
using TeachersSideAPI.Domain.Models;

namespace TeachersSideAPI.Persistence.Repositories.Implementation;

public class EventRepository : IEventRepository
{
    private readonly TeachersSideContext _context;

    public EventRepository(TeachersSideContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<Event>> GetAllAsync()
    {
        return await _context.Events
            .Include(x => x.Creator)
            .ToListAsync();
    }

    public async Task<Event?> GetAsync(int id)
    {
        return await _context.Events
            .Include(x => x.Creator)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> SaveAsync(Event evt)
    {
        await _context.Events.AddAsync(evt);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Event evt)
    {
        _context.Events.Remove(evt);
        return await _context.SaveChangesAsync() > 0;
    }
    
    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}