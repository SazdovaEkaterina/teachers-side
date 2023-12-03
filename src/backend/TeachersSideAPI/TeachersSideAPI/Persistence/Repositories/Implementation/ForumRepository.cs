namespace TeachersSideAPI.Persistence.Repositories.Implementation;

public class ForumRepository : IForumRepository
{
    private readonly TeachersSideContext _context;

    public ForumRepository(TeachersSideContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
}