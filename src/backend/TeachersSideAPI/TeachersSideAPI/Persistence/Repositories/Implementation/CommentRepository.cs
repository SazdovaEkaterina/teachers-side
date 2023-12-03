namespace TeachersSideAPI.Persistence.Repositories.Implementation;

public class CommentRepository : ICommentRepository
{
    private readonly TeachersSideContext _context;

    public CommentRepository(TeachersSideContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
}