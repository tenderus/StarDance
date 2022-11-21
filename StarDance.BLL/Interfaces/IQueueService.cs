using StarDance.Common.Dtos.LessonDtos;
using StarDance.Common.Dtos.QueueDtos;

namespace StarDance.BLL.Interfaces;

public interface IQueueService
{
    Task AddClientToQueueAsync(LessonClientCreateDto dto, CancellationToken cancellationToken);
    Task DeleteClientFromQueueAsync(LessonClientCreateDto dto, CancellationToken cancellationToken);
    Task<bool> IsClientInQueueAsync(LessonClientCreateDto dto, CancellationToken cancellationToken);
    Task<int> GetOrderOfQueueAsync(LessonClientCreateDto dto, CancellationToken cancellationToken);
}