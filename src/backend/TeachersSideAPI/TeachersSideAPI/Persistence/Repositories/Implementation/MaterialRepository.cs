using Microsoft.EntityFrameworkCore;
using TeachersSideAPI.Domain.Enums;
using TeachersSideAPI.Domain.Models;

namespace TeachersSideAPI.Persistence.Repositories.Implementation;

public class MaterialRepository : IMaterialRepository
{
    private readonly TeachersSideContext _context;

    public MaterialRepository(TeachersSideContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task<IEnumerable<Material>> GetAllAsync()
    {
        return await _context.Materials
            .Include(x => x.Creator)
            .Include(x => x.Subject)
            .ToListAsync();
    }

    public async Task<Material?> GetAsync(int id)
    {
        return await _context.Materials
            .Include(x => x.Creator)
            .Include(x => x.Subject)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> SaveAsync(Material material)
    {
        await _context.Materials.AddAsync(material);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Material material)
    {
        _context.Materials.Remove(material);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<Material>> GetAllBySubjectNameAndCategoryAsync(string subjectName, Category category)
    {
        return await _context.Materials
            .Where(x => x.Subject.Name.Equals(subjectName) && x.Subject.Category == category)
            .ToListAsync();
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}