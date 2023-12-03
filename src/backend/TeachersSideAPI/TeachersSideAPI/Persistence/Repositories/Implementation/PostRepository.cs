namespace TeachersSideAPI.Persistence.Repositories.Implementation;

public class PostRepository : IPostRepository
{
    private readonly TeachersSideContext _context;

    public PostRepository(TeachersSideContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
}