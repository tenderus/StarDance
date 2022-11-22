using AutoMapper;
using StarDance.Common.Dtos.TeacherDtos;
using StarDance.Common.Helpers;
using StarDance.Domain;

namespace StarDance.BLL.Profiles;

public class TeacherProfile: Profile
{
    public TeacherProfile()
    {
        CreateMap<Teacher, TeacherReadDto>()
            .ForMember(x => x.User, y => y.MapFrom(z => z.User));

        CreateMap<TeacherUpdateDto, Teacher>();
        
        CreateMap<TeacherPartialUpdateDto, Teacher>();

        CreateMap<PaginatedResult<Teacher>, PaginatedResult<TeacherReadDto>>();
    }
}