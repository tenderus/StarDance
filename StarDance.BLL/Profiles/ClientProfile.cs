using AutoMapper;
using StarDance.Common.Dtos.ClientDtos;
using StarDance.Domain;

namespace StarDance.BLL.Profiles;

public class ClientProfile : Profile
{
    public ClientProfile()
    {
        CreateMap<ClientRegisterDto, User>();
        CreateMap<User, ClientReadDto>()
            .ForMember(x => x.LessonsOfClientDtos, y => y.MapFrom(z => z.Lessons));
        
        CreateMap<ClientUpdateDto, User>();
        CreateMap<ClientPartialUpdateDto, User>();
    }
}