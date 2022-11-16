using AutoMapper;
using StarDance.Common.Dtos.LessonDtos;
using StarDance.Common.Dtos.TeacherDtos;
using StarDance.Domain;

namespace StarDance.BLL.Profiles;

public class TeacherProfile: Profile
{
    public TeacherProfile()
    {
        CreateMap<Teacher, TeacherReadDto>()
            .ForMember(x => x.User, y => y.MapFrom(z => z.User));
        
        CreateMap<TeacherUpdateDto, Teacher>()
            .ForMember(x => x.DanceType, y => y.MapFrom(z => z.DanceTypeId));
        CreateMap<TeacherPartialUpdateDto, Teacher>()
            .ForMember(x => x.DanceType, y => y.MapFrom(z => z.DanceTypeId));
    }
}