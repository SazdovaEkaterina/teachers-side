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

    public async Task<IEnumerable<Event>> GetAll()
    {
        return await _context.Events
            .Include(x => x.Creator)
            .ToListAsync();
    }

    public async Task<Event?> Get(int id)
    {
        return await _context.Events
            .Include(x => x.Creator)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> Save(Event evt)
    {
        await _context.Events.AddAsync(evt);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(Event evt)
    {
        _context.Events.Remove(evt);
        return await _context.SaveChangesAsync() > 0;
    }
}