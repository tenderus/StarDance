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
    public async Task<IActionResult> GetOrderOfQueue([FromBody] LessonClientCreateDto dto)
    {
        var numberOfQueueOrder = await _queueService.GetOrderOfQueueAsync(dto);

        return Ok(numberOfQueueOrder);

    }
    
    [HttpPost("clientsQueues")]
    public async Task<IActionResult> AddClientToQueue([FromBody] LessonClientCreateDto lessonClientCreateDto, CancellationToken cancellationToken)
    {
        await _queueService.AddClientToQueueAsync(lessonClientCreateDto, cancellationToken);
        return Ok();
    }
        
    [HttpDelete("clientsQueue")]
    public async Task<IActionResult> DeleteClientFromQueue([FromBody] LessonClientCreateDto lessonClientCreateDto, CancellationToken cancellationToken)
    {
        await _queueService.DeleteClientFromQueueAsync(lessonClientCreateDto, cancellationToken);
        return Ok();
    }
    
    [HttpPost("clientIsInQueue")]
    public async Task<IActionResult> CheckClientIsInQueue([FromBody] LessonClientCreateDto lessonClientCreateDto)
    {
        var result = await _queueService.CheckClientIsInQueueAsync(lessonClientCreateDto);
        return Ok(result);
    }
}