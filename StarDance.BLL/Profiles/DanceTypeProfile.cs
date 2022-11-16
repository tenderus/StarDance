using AutoMapper;
using StarDance.Common.Dtos.DanceTypeDtos;
using StarDance.Domain;

namespace StarDance.BLL.Profiles;

public class DanceTypeProfile : Profile
{
    public DanceTypeProfile()
    {
        CreateMap<DanceType, DanceTypeReadDto>()
            .ForMember(x => x.TeachersReadDtos, y => y.MapFrom(z => z.Teachers));;
    }
}