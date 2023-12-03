namespace TeachersSideAPI.Persistence.Repositories.Implementation;

public class MaterialRepository : IMaterialRepository
{
    private readonly TeachersSideContext _context;

    public MaterialRepository(TeachersSideContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
}