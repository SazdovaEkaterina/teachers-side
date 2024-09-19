using TeachersSideAPI.Domain.DTO;

namespace TeachersSideAPI.Service;

public interface ICommentService
{
    Task<IEnumerable<CommentDto>> GetAllForPostAsync(int postId);
    Task<bool> SaveAsync(CommentDto commentDto);
    Task<bool> DeleteAsync(int id);
    Task<bool> EditAsync(int id, CommentDto commentDto);

}