using TeachersSideAPI.Persistence.Repositories;

namespace TeachersSideAPI.Service.Implementation;

public class ForumService : IForumService
{
    private readonly IForumRepository _forumRepository;

    public ForumService(IForumRepository forumRepository)
    {
        _forumRepository = forumRepository ?? throw new ArgumentNullException(nameof(forumRepository));
    }
}