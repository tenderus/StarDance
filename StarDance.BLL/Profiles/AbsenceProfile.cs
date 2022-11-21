using AutoMapper;
using StarDance.Common.Dtos.AbsenceDtos;
using StarDance.Domain;

namespace StarDance.BLL.Profiles;

public class AbsenceProfile : Profile
{
    public AbsenceProfile()
    {
        CreateMap<AbsenceUpdateDto, Absence>();
    }
}