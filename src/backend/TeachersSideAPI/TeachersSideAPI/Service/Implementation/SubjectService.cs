using TeachersSideAPI.Domain.Models;
using TeachersSideAPI.Persistence.Repositories;

namespace TeachersSideAPI.Service.Implementation;

public class SubjectService : ISubjectService
{
    private readonly ISubjectRepository _subjectRepository;

    public SubjectService(ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository ?? throw new ArgumentNullException(nameof(subjectRepository));
    }

    public async Task<IEnumerable<Subject>> GetSubjectsAsync()
    {
        return await _subjectRepository.GetSubjectsAsync();
    }
}