using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TeachersSideAPI.Domain.DTO;
using TeachersSideAPI.Domain.Models;
using TeachersSideAPI.Persistence.Repositories;
using TeachersSideAPI.Service.Exceptions;

namespace TeachersSideAPI.Service.Implementation;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IPostRepository _postRepository;
    private readonly IForumRepository _forumRepository;

    private readonly IMapper _mapper;
    private readonly UserManager<Teacher> _userManager;

    public CommentService(
        ICommentRepository commentRepository,
        IMapper mapper,
        UserManager<Teacher> userManager,
        IForumRepository forumRepository,
        IPostRepository postRepository
    )
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _forumRepository = forumRepository ?? throw new ArgumentNullException(nameof(forumRepository));
        _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
        _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
    }

    public async Task<IEnumerable<CommentDto>> GetAllForPostAsync(int postId)
    {
        var comments = await _commentRepository.GetAllByPostIdAsync(postId);
        return _mapper.Map<IEnumerable<CommentDto>>(comments);
    }

    public async Task<bool> SaveAsync(CommentDto commentDto)
    {
        var comment = _mapper.Map<Comment>(commentDto);
        comment.Post.Forum = await _forumRepository.GetAsync(commentDto.Post.Forum.Id)
                             ?? throw new ForumNotFoundException(
                                 $"Forum with ID: {commentDto.Post.Forum.Id} not found.");
        comment.Post = await _postRepository.GetAsync(commentDto.Post.Id)
                       ?? throw new ForumNotFoundException($"Post with ID: {commentDto.Post.Id} not found.");
        comment.Creator = await _userManager.FindByEmailAsync(commentDto.Creator.Email)
                          ?? throw new UserNotFoundException($"User with email {commentDto.Creator.Email} not found");
        comment.Title = comment.Title;
        comment.Content = comment.Content;
        comment.DateCreated = DateTime.UtcNow;
        comment.LastEdited = DateTime.UtcNow;
        return await _commentRepository.SaveAsync(comment);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var comment = await _commentRepository.GetAsync(id);

        if (comment == null)
            return false;

        return await _commentRepository.DeleteAsync(comment);
    }

    public async Task<bool> EditAsync(int id, CommentDto commentDto)
    {
        var comment = await _commentRepository.GetAsync(id);

        if (comment == null)
            return false;
        
        comment.Post.Forum = await _forumRepository.GetAsync(commentDto.Post.Forum.Id)
                             ?? throw new ForumNotFoundException(
                                 $"Forum with ID: {commentDto.Post.Forum.Id} not found.");
        comment.Post = await _postRepository.GetAsync(commentDto.Post.Id)
                       ?? throw new ForumNotFoundException($"Post with ID: {commentDto.Post.Id} not found.");
        comment.Creator = await _userManager.FindByEmailAsync(commentDto.Creator.Email)
                          ?? throw new UserNotFoundException($"User with email {commentDto.Creator.Email} not found");
        comment.Title = comment.Title;
        comment.Content = comment.Content;
        comment.DateCreated = DateTime.UtcNow;
        comment.LastEdited = DateTime.UtcNow;

        return await _commentRepository.SaveChangesAsync();
    }
}