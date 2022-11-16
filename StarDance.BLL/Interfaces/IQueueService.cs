using StarDance.Common.Dtos.LessonDtos;
using StarDance.Common.Dtos.QueueDtos;

namespace StarDance.BLL.Interfaces;

public interface IQueueService
{
    Task AddClientToQueueAsync(LessonClientCreateDto dto, CancellationToken cancellationToken);
    Task DeleteClientFromQueueAsync(LessonClientCreateDto dto, CancellationToken cancellationToken);
    Task<bool> CheckClientIsInQueueAsync(LessonClientCreateDto dto);
    Task<int> GetOrderOfQueueAsync(LessonClientCreateDto dto);
}