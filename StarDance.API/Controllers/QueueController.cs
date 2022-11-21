using Microsoft.AspNetCore.Mvc;
using StarDance.BLL.Interfaces;
using StarDance.Common.Dtos.LessonDtos;
using StarDance.Common.Dtos.QueueDtos;

namespace StarDance.API.Controllers;

[Route("api/queues")]
[ApiController]
public class QueueController : ControllerBase
{
    private readonly IQueueService  _queueService;

    public QueueController(IQueueService queueService)
    {
        _queueService = queueService;
    }

    [HttpPost("clientQueueOrder")]
    public async Task<int> GetOrderOfQueue(LessonClientCreateDto dto, CancellationToken cancellationToken)
    {
        var numberOfQueueOrder = await _queueService.GetOrderOfQueueAsync(dto, cancellationToken);

        return numberOfQueueOrder;

    }
    
    [HttpPost("clientsQueues")]
    public async Task AddClientToQueue(LessonClientCreateDto lessonClientCreateDto, CancellationToken cancellationToken)
    {
        await _queueService.AddClientToQueueAsync(lessonClientCreateDto, cancellationToken);
    }
        
    [HttpDelete("clientsQueue")]
    public async Task DeleteClientFromQueue(LessonClientCreateDto lessonClientCreateDto, CancellationToken cancellationToken)
    {
        await _queueService.DeleteClientFromQueueAsync(lessonClientCreateDto, cancellationToken);
    }
    
    [HttpPost("clientIsInQueue")]
    public async Task<bool> CheckClientIsInQueue(LessonClientCreateDto lessonClientCreateDto, CancellationToken cancellationToken)
    {
        var result = await _queueService.IsClientInQueueAsync(lessonClientCreateDto, cancellationToken);
        return result;
    }
}