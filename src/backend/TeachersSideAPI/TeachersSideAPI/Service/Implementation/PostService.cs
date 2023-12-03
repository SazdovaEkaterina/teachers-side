using TeachersSideAPI.Persistence.Repositories;

namespace TeachersSideAPI.Service.Implementation;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;

    public PostService(IPostRepository postRepository)
    {
        _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
    }
}