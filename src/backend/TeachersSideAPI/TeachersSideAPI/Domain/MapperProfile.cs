using AutoMapper;
using TeachersSideAPI.Domain.DTO;
using TeachersSideAPI.Domain.Models;

namespace TeachersSideAPI.Domain;

public class MapperProfile : Profile
{
    public MapperProfile(string webRootPath)
    {
        CreateMap<EventDto, Event>();
        CreateMap<Event, EventDto>();
        CreateMap<Teacher, TeacherDto>();
        CreateMap<TeacherDto, Teacher>();
        CreateMap<MaterialDto, Material>()
            .ForMember(dest => dest.FilePath, opt => opt.MapFrom(src => ConvertFileToString(src.File, webRootPath)));
        CreateMap<Material, MaterialDto>()
            .ForMember(dest => dest.FilePath, opt => opt.MapFrom(src => src.FilePath))
            .ForMember(dest => dest.File, opt => opt.MapFrom(src => ConvertStringToFile(src.FilePath, webRootPath)));
        CreateMap<Subject, SubjectDto>();
        CreateMap<SubjectDto, Subject>();
        CreateMap<Post, PostDto>();
        CreateMap<PostDto, Post>();
        CreateMap<Forum, ForumDto>();
        CreateMap<ForumDto, Forum>();
        CreateMap<Comment, CommentDto>();
        CreateMap<CommentDto, Comment>();
    }
    
    private string ConvertFileToString(IFormFile file, string webRootPath)
    {
        string path = "";
        if (file.Length > 0)
        {
            var fileName = Path.GetFileName(file.FileName);

            var filePath = Path.Combine(webRootPath, "wwwroot", "files", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyToAsync(stream).Wait();
            }

            path = $"/files/{fileName}";
        }

        return path;
    }
    
    private IFormFile ConvertStringToFile(string filePath, string webRootPath)
    {
        var fullPath = Path.Combine(webRootPath, "wwwroot", filePath.TrimStart('/'));

        if (File.Exists(fullPath))
        {
            var fileBytes = File.ReadAllBytes(fullPath);

            var stream = new MemoryStream(fileBytes);

            var fileName = Path.GetFileName(fullPath);

            var formFile = new FormFile(stream, 0, stream.Length, fileName, fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/octet-stream"
            };

            return formFile;
        }

        return null;
    }
}