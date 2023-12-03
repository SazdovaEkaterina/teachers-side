namespace TeachersSideAPI.Persistence.Repositories.Implementation;

public class EventRepository : IEventRepository
{
    private readonly TeachersSideContext _context;

    public EventRepository(TeachersSideContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
}