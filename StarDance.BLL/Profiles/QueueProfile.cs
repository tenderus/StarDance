using AutoMapper;
using StarDance.Common.Dtos.QueueDtos;
using StarDance.Domain;

namespace StarDance.BLL.Profiles;

public class QueueProfile : Profile
{
    public QueueProfile()
    {
        CreateMap<Queue, QueueReadDto>();
        CreateMap<QueueUpdateDto, Queue>();
    }
}