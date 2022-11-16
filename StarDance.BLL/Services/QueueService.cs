using AutoMapper;
using StarDance.BLL.Exceptions;
using StarDance.BLL.Interfaces;
using StarDance.Common.Dtos.LessonDtos;
using StarDance.Common.Dtos.QueueDtos;
using StarDance.DAL.Interfaces;
using StarDance.Domain;

namespace StarDance.BLL.Services;

public class QueueService : IQueueService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Queue> _repository;
    private readonly IRepository<User> _clientRepository;
    private readonly IRepository<Lesson> _lessonRepository;

    public QueueService(IRepository<Queue> repo, IMapper mapper, IRepository<User> clientRepository, IRepository<Lesson> lessonRepository)
    {
        _repository = repo;
        _mapper = mapper;
        _lessonRepository = lessonRepository;
        _clientRepository = clientRepository;
    }

    public async Task<int> GetOrderOfQueueAsync(LessonClientCreateDto dto)
    {
        var client = await _clientRepository.GetByIdAsync(dto.ClientId);
        if (client == null)
        {
            throw new ClientDoesNotExistException("Client with such id doesn't exists");
        }

        var lesson = await _lessonRepository.GetByIdAsync(dto.LessonId, lesson => lesson.Clients,
            lesson => lesson.Queues);
        if (lesson == null)
        {
            throw new LessonDoesNotExistException("Lesson with such id doesn't exists");
        }

        var queue = await _repository.FindAsync(queue =>
            queue.ClientId == dto.ClientId && queue.LessonId == dto.LessonId);

        if (queue != null)
        {
            return queue.NumberOfOrder;
        }

        return 0;
    }
    
    public async Task<bool> CheckClientIsInQueueAsync(LessonClientCreateDto dto)
    {
        var client = await _clientRepository.GetByIdAsync(dto.ClientId);
        if (client == null)
        {
            throw new ClientDoesNotExistException("Client with such id doesn't exists");
        }

        var lesson = await _lessonRepository.GetByIdAsync(dto.LessonId, lesson => lesson.Clients, lesson => lesson.Queues);
        if (lesson == null)
        {
            throw new LessonDoesNotExistException("Lesson with such id doesn't exists");
        }
        
        if (lesson.Queues.FirstOrDefault(x => x.ClientId == dto.ClientId) is not null) return true;
        return false;
    }


    public async Task AddClientToQueueAsync(LessonClientCreateDto lessonClientCreateDto, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetByIdAsync(lessonClientCreateDto.ClientId);
        if (client == null)
        {
            throw new ClientDoesNotExistException("Client with such id doesn't exists");
        }

        var lesson = await _lessonRepository.GetByIdAsync(lessonClientCreateDto.LessonId, lesson => lesson.Clients, lesson => lesson.Queues);
        if (lesson == null)
        {
            throw new LessonDoesNotExistException("Lesson with such id doesn't exists");
        }

        var queueModel = new QueueUpdateDto();
        queueModel.ClientId = lessonClientCreateDto.ClientId;
        queueModel.LessonId = lessonClientCreateDto.LessonId;
        queueModel.NumberOfOrder = lesson.Queues.Count + 1;
        if (lesson.Queues.FirstOrDefault(x => x.ClientId == lessonClientCreateDto.ClientId) is not null)
        {
            throw new ClientAlreadyRegisteredAtLessonException("Client is already in queue");
        }
        
        var queue = _mapper.Map<Queue>(queueModel);
        lesson.Queues.Add(queue);
        await _lessonRepository.UpdateAsync(lesson, cancellationToken);
        await _lessonRepository.SaveChangesAsync(cancellationToken);

    }

    public async Task DeleteClientFromQueueAsync(LessonClientCreateDto lessonClientCreateDto, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetByIdAsync(lessonClientCreateDto.ClientId);
        if (client == null)
        {
            throw new ClientDoesNotExistException("Client with such id doesn't exists");
        }

        var lesson = await _lessonRepository.GetByIdAsync(lessonClientCreateDto.LessonId, lesson => lesson.Clients,
            lesson => lesson.Queues);
        if (lesson == null)
        {
            throw new LessonDoesNotExistException("Lesson with such id doesn't exists");
        }

        if (lesson.Queues is not null)
        {
            var queue = lesson.Queues.FirstOrDefault(x => x.ClientId == lessonClientCreateDto.ClientId);
            foreach (var lessonQueue in lesson.Queues)
            {
                if (lessonQueue.Id > queue.Id)
                {
                    lessonQueue.NumberOfOrder--;
                }
            }

            if (queue != null)
            {
                await _repository.DeleteAsync(queue, cancellationToken);
                await _repository.SaveChangesAsync(cancellationToken);
            }
        }
    }
}