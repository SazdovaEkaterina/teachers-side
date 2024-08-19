using Microsoft.EntityFrameworkCore;
using TeachersSideAPI.Domain.Enums;
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

    public async Task<Subject?> GetBySubjectNameAndCategoryAsync(string name, Category category)
    {
        return await _context.Subjects
            .Include(x => x.Teachers)
            .FirstOrDefaultAsync(x => x.Name.Equals(name) && x.Category == category);
    }

    public async Task<Subject?> GetAsync(int id)
    {
        return await _context.Subjects
            .Include(x => x.Teachers)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}