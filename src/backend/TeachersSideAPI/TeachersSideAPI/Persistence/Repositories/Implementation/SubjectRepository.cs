using Microsoft.EntityFrameworkCore;
using TeachersSideAPI.Domain.Models;

namespace TeachersSideAPI.Persistence.Repositories.Implementation;

public class SubjectRepository : ISubjectRepository
{
    private readonly TeachersSideContext _context;

    public SubjectRepository(TeachersSideContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<Subject>> GetSubjectsAsync()
    {
        return await _context.Subjects.ToListAsync();
    }
}