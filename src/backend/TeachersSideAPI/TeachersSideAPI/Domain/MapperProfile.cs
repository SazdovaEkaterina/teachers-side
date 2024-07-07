using AutoMapper;
using TeachersSideAPI.Domain.DTO;
using TeachersSideAPI.Domain.Models;

namespace TeachersSideAPI.Domain;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<EventDto, Event>();
        CreateMap<Event, EventDto>();
        CreateMap<Teacher, TeacherDto>();
        CreateMap<TeacherDto, Teacher>();
        CreateMap<MaterialDto, Material>();
        CreateMap<Material, MaterialDto>();
        CreateMap<Subject, SubjectDto>();
        CreateMap<SubjectDto, Subject>();
    }
}