using AutoMapper;
using TeachersSideAPI.Domain.DTO;
using TeachersSideAPI.Domain.Models;
using TeachersSideAPI.Persistence.Repositories;
using TeachersSideAPI.Service.Exceptions;

namespace TeachersSideAPI.Service.Implementation;

public class ForumService : IForumService
{
    private readonly IForumRepository _forumRepository;
    private readonly IMapper _mapper;
    private readonly ISubjectRepository _subjectRepository;
    
    public ForumService(IForumRepository forumRepository, IMapper mapper, ISubjectRepository subjectRepository)
    {
        _forumRepository = forumRepository ?? throw new ArgumentNullException(nameof(forumRepository));
        _mapper = mapper;
        _subjectRepository = subjectRepository ?? throw new ArgumentNullException(nameof(subjectRepository));
    }
    
    public async Task<IEnumerable<ForumDto>> GetAllAsync()
    {
        IEnumerable<Forum> forums = await _forumRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ForumDto>>(forums);
    }

    public async Task<ForumDto?> GetAsync(int id)
    {
        var forum = await _forumRepository.GetAsync(id);
        
        return forum != null ? _mapper.Map<ForumDto>(forum) : null;
    }

    public async Task<bool> SaveAsync(ForumDto forumDto)
    {
        var forum = _mapper.Map<Forum>(forumDto);
        forum.Subject = await _subjectRepository.GetAsync(forumDto.Subject.Id)
                        ?? throw new SubjectNotFoundException($"Subject with id: {forumDto.Subject.Id} not found.");
        return await _forumRepository.SaveAsync(forum);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var forum = await _forumRepository.GetAsync(id);

        if (forum == null)
            return false;
        
        return await _forumRepository.DeleteAsync(forum);
    }

    public async Task<bool> EditAsync(int id, ForumDto forumDto)
    {
        var forum = await _forumRepository.GetAsync(id);

        if (forum == null)
            return false;
        
        forum.Subject = await _subjectRepository.GetAsync(forumDto.Subject.Id)
                        ?? throw new SubjectNotFoundException($"Subject with id: {forumDto.Subject.Id} not found.");
        forum.Posts = _mapper.Map<List<Post>>(forumDto.Posts);

        return await _forumRepository.SaveChangesAsync();
    }
}