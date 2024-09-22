using AutoMapper;
using TeachersSideAPI.Domain.DTO;
using TeachersSideAPI.Domain.Models;

namespace TeachersSideAPI.Domain;

public class MapperProfile : Profile
{
    public MapperProfile(string webRootPath)
    {
        CreateMap<EventDto, Event>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => ConvertToString(src.Image, webRootPath)));
        CreateMap<Event, EventDto>()
            .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.Image))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => (IFormFile?)null));
        CreateMap<Teacher, TeacherDto>();
        CreateMap<TeacherDto, Teacher>();
        CreateMap<MaterialDto, Material>();
        CreateMap<Material, MaterialDto>();
        CreateMap<Subject, SubjectDto>();
        CreateMap<SubjectDto, Subject>();
        CreateMap<Post, PostDto>();
        CreateMap<PostDto, Post>();
        CreateMap<Forum, ForumDto>();
        CreateMap<ForumDto, Forum>();
        CreateMap<Comment, CommentDto>();
        CreateMap<CommentDto, Comment>();
    }

    private string ConvertToString(IFormFile file, string webRootPath)
    {
        string path = "";
        if (file.Length > 0)
        {
            var fileName = Path.GetFileName(file.FileName);

            var filePath = Path.Combine(webRootPath, "wwwroot", "images", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyToAsync(stream).Wait();
            }

            path = $"/images/{fileName}";
        }

        return path;
    }
}