using AutoMapper;
using StarDance.Common.Dtos.LessonDtos;
using StarDance.Common.Helpers;
using StarDance.Domain;

namespace StarDance.BLL.Profiles;

public class LessonProfile : Profile
{
    public LessonProfile()
    {
        CreateMap<Lesson, LessonReadDto>()
            .ForMember(x => x.TeacherReadDto, y => y.MapFrom(z => z.Teacher))
            .ForMember(x => x.RoomReadDto, y => y.MapFrom(z => z.Room))
            .ForMember(x => x.Clients, y => y.MapFrom(z => z.Clients));

        CreateMap<LessonUpdateDto, Lesson>()
            .ForMember(x => x.RoomId, y => y.MapFrom(z => z.RoomPartialUpdateDtoId))
            .ForMember(x => x.TeacherId, y => y.MapFrom(z => z.TeacherPartialUpdateDtoId));
        CreateMap<LessonPartialUpdateDto, Lesson>();

        CreateMap<LessonsOfClientDto, LessonReadDto>()
            .ForMember(x => x.TeacherReadDto, y => y.MapFrom(z => z.TeacherReadDto))
            .ForMember(x => x.RoomReadDto, y => y.MapFrom(z => z.RoomReadDto));

        CreateMap<Lesson, LessonsOfClientDto>()
            .ForMember(x => x.TeacherReadDto, y => y.MapFrom(z => z.Teacher))
            .ForMember(x => x.RoomReadDto, y => y.MapFrom(z => z.Room));

        CreateMap<PaginatedResult<Lesson>, PaginatedResult<LessonReadDto>>();
    }
}