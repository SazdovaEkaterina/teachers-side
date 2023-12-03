using TeachersSideAPI.Persistence.Repositories;

namespace TeachersSideAPI.Service.Implementation;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;

    public CommentService(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
    }
}