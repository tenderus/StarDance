using AutoMapper;
using StarDance.Common.Dtos.RoomDtos;
using StarDance.Domain;

namespace StarDance.BLL.Profiles;

public class RoomProfile : Profile
{
    public RoomProfile()
    {
        CreateMap<Room, RoomReadDto>();
        CreateMap<Room, RoomUpdateDto>();
        CreateMap<Room, RoomPartialUpdateDto>();

    }
}