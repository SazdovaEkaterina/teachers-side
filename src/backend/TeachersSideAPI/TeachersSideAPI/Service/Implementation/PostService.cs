using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TeachersSideAPI.Domain.DTO;
using TeachersSideAPI.Domain.Models;
using TeachersSideAPI.Persistence.Repositories;
using TeachersSideAPI.Service.Exceptions;

namespace TeachersSideAPI.Service.Implementation;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IForumRepository _forumRepository;
    private readonly IMapper _mapper;
    private readonly UserManager<Teacher> _userManager;

    public PostService(IPostRepository postRepository, IForumRepository forumRepository, IMapper mapper, UserManager<Teacher> userManager)
    {
        _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
        _forumRepository = forumRepository ?? throw new ArgumentNullException(nameof(forumRepository));
        _mapper = mapper;
        _userManager = userManager;
    }
    
    public async Task<IEnumerable<PostDto>> GetAllAsync()
    {
        IEnumerable<Post> posts = await _postRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<PostDto>>(posts);
    }

    public async Task<PostDto?> GetAsync(int id)
    {
        var post = await _postRepository.GetAsync(id);
        
        return post != null ? _mapper.Map<PostDto>(post) : null;
    }

    public async Task<bool> SaveAsync(PostDto postDto)
    {
        var post = _mapper.Map<Post>(postDto);
        post.Forum = await _forumRepository.GetAsync(postDto.Forum.Id) 
                       ?? throw new ForumNotFoundException($"Forum with ID: {post.Forum.Id} not found.");
        post.Creator = await _userManager.FindByEmailAsync(post.Creator.Email)
                       ?? throw new UserNotFoundException($"User with email {post.Creator.Email} not found.");
        post.Title = postDto.Title;
        post.Content = postDto.Content;
        post.DateCreated = DateTime.UtcNow;
        post.LastEdited = DateTime.UtcNow;
        return await _postRepository.SaveAsync(post);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var post = await _postRepository.GetAsync(id);

        if (post == null)
            return false;
        
        return await _postRepository.DeleteAsync(post);
    }

    public async Task<bool> EditAsync(int id, PostDto postDto)
    {
        var post = await _postRepository.GetAsync(id);

        if (post == null)
            return false;
        
        post.Forum = await _forumRepository.GetAsync(postDto.Forum.Id) 
                     ?? throw new ForumNotFoundException($"Forum with ID: {post.Forum.Id} not found.");
        post.Creator = await _userManager.FindByEmailAsync(post.Creator.Email)
                       ?? throw new UserNotFoundException($"User with email {post.Creator.Email} not found.");
        post.Title = postDto.Title;
        post.Content = postDto.Content;
        post.LastEdited = DateTime.UtcNow;

        return await _postRepository.SaveChangesAsync();
    }
    
    public async Task<IEnumerable<PostDto>> GetAllPostsByForumAsync(int forumId)
    {
        var forum =  await _forumRepository.GetAsync(forumId) 
                       ?? throw new ForumNotFoundException($"Forum with ID: {forumId} not found.");
        var posts = await _postRepository.GetAllByForumAsync(forum);
        return _mapper.Map<IEnumerable<PostDto>>(posts);
    }
}