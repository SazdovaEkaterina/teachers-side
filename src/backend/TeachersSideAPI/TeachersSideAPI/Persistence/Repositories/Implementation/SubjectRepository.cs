namespace TeachersSideAPI.Persistence.Repositories.Implementation;

public class SubjectRepository : ISubjectRepository
{
    private readonly TeachersSideContext _context;

    public SubjectRepository(TeachersSideContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
}