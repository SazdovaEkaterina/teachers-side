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
    }
}