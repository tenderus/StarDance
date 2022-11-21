using AutoMapper;
using StarDance.BLL.Exceptions;
using StarDance.BLL.Interfaces;
using StarDance.Common.Dtos.LessonDtos;
using StarDance.Common.Helpers;
using StarDance.DAL.Interfaces;
using StarDance.Domain;

namespace StarDance.BLL.Services;

public class LessonService : ILessonService
{
    private readonly IMapper _mapper;
    private readonly IRepository<User> _clientRepository;
    private readonly ILessonRepository _lessonRepository;
    private readonly IRepository<Queue> _queueRepository;
    private readonly IQueueService _queueService;

    public LessonService(IMapper mapper, IRepository<User> clientRepository, ILessonRepository lessonRepository, IRepository<Queue> queueRepository, IQueueService queueService)
    {
        _mapper = mapper;
        _lessonRepository = lessonRepository;
        _clientRepository = clientRepository;
        _queueRepository = queueRepository;
        _queueService = queueService;
    }

    public async Task<LessonReadDto> CreateLessonAsync(LessonUpdateDto dto, CancellationToken cancellationToken)
    {

        var lessonModel = _mapper.Map<Lesson>(dto);
        await _lessonRepository.AddAsync(lessonModel, cancellationToken);
        await _lessonRepository.SaveChangesAsync(cancellationToken);
        var lessonReadDto = _mapper.Map<LessonReadDto>(lessonModel);
        return lessonReadDto;
    }

    public async Task<bool> DeleteLessonAsync(int id, CancellationToken cancellationToken)
    {
        var lesson = await _lessonRepository.GetByIdAsync(id, cancellationToken);
        if (lesson != null)
        {
            _lessonRepository.Delete(lesson);
            await _lessonRepository.SaveChangesAsync(cancellationToken);
            return true;
        }
        return false;
    }
    
    public async Task<List<LessonReadDto>> GetLessonsByClientId(int id, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetByIdAsync(id, cancellationToken, client => client.Lessons);
        var lessonsOfClient = await _lessonRepository.FindAllWithIncludeAsync(lesson => lesson.Clients.Contains(client),
            cancellationToken,
            lesson => lesson.Teacher.User,
            lesson => lesson.Clients,
            lesson => lesson.Room,
            lesson => lesson.Teacher.DanceType);
        var lessonReadDtos = _mapper.Map<List<LessonReadDto>>(lessonsOfClient);

        return lessonReadDtos;

    }
    
    public async Task<PaginatedResult<LessonReadDto>> GetPagedResult(PagedRequest pagedRequest, CancellationToken cancellationToken)
    {
        var lessonsList = await _lessonRepository.GetPagedDataWithInclude(pagedRequest, cancellationToken,
            x=> x.Clients,
            x => x.Teacher.User,
            x => x.Teacher.DanceType,
            x => x.Room);
        var lessonListDtos = _mapper.Map<PaginatedResult<LessonReadDto>>(lessonsList);
        return lessonListDtos;
    }
    
    public async Task<List<LessonReadDto>> GetAllLessonsAsync(CancellationToken cancellationToken)
    {
        var lessons =  await _lessonRepository.GetAllAsync(cancellationToken,
            lesson => lesson.Teacher.User,
            lesson => lesson.Clients,
            lesson => lesson.Teacher.DanceType,
            lesson => lesson.Room);
        
        var lessonReadDtos = _mapper.Map<List<LessonReadDto>>(lessons);
        return lessonReadDtos;
    }
    

    public async Task<LessonReadDto> GetLessonByIdAsync(int id, CancellationToken cancellationToken)
    {
        var lesson = await _lessonRepository.GetByIdAsync(id, cancellationToken, lesson => lesson.Teacher.User,
            lesson => lesson.Room,
            lesson => lesson.Teacher.DanceType);
        var lessonDto = _mapper.Map<LessonReadDto>(lesson);
        return lessonDto;
    }

    public async Task<LessonReadDto> GetLessonByDancetypeAsync(string dancetype, CancellationToken cancellationToken)
    {
        var lesson = await _lessonRepository.GetByDancetypeAsync(dancetype, cancellationToken,
            lesson => lesson.Teacher.User,
            lesson => lesson.Room);
        var lessonDto = _mapper.Map<LessonReadDto>(lesson);
        return lessonDto;
    }
    
    public async Task<LessonReadDto> UpdateLessonAsync(int id, LessonUpdateDto dto, CancellationToken cancellationToken)
    {
        var lessonModel = await _lessonRepository.GetByIdAsync(id, cancellationToken);
        if (lessonModel == null)
        {
            throw new Exception("Lesson with such id doesn't exists");
        }
        
        _mapper.Map(dto,lessonModel);
        _lessonRepository.Update(lessonModel);
        await _lessonRepository.SaveChangesAsync(cancellationToken);

        var lessonReadDto = _mapper.Map<LessonReadDto>(lessonModel);
        return lessonReadDto;
    }

    public async Task<LessonReadDto> AddClientToLessonAsync(LessonClientCreateDto lessonClientCreateDto, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetByIdAsync(lessonClientCreateDto.ClientId, cancellationToken);
        if (client == null)
        {
            throw new ClientDoesNotExistException("Client with such id doesn't exists");
        }
        
        var lesson = await _lessonRepository.GetByIdAsync(lessonClientCreateDto.LessonId, cancellationToken, 
            lesson => lesson.Clients, lesson => lesson.Queues, lesson => lesson.Room);
        if (lesson == null)
        {
            throw new LessonDoesNotExistException("Lesson with such id doesn't exists");
        }
        

        if (lesson.Room.Capacity > lesson.Clients.Count)
        {
            lesson.Clients.Add(client);
            _lessonRepository.Update(lesson);
            await _lessonRepository.SaveChangesAsync(cancellationToken);
        }
        else
        {
            throw new NoFreePlacesRemainedAtLessonException(
                "There's no free place left for this lesson");
        }

        return _mapper.Map<LessonReadDto>(lesson);
    }
    
    public async Task DeleteClientFromLessonAsync(LessonClientCreateDto lessonClientCreateDto, CancellationToken cancellationToken)
        {
            
            var lesson = await _lessonRepository.GetByIdAsync(lessonClientCreateDto.LessonId, cancellationToken, lesson => lesson.Clients,
                lesson => lesson.Queues);
            if (lesson == null)
            {
                throw new LessonDoesNotExistException("Lesson with such id doesn't exists");
            }

            var queueOfClientUnderFirstNum = lesson.Queues.FirstOrDefault(x => x.LessonId == lessonClientCreateDto.LessonId && x.NumberOfOrder == 1);
            var clientUnderFirstNum = await _clientRepository.FindAsync(x => queueOfClientUnderFirstNum != null && x.Id == queueOfClientUnderFirstNum.ClientId, cancellationToken);
            
            if (lesson.Clients is not null)
            {
                var clientOnLesson = lesson.Clients.FirstOrDefault(x => x.Id == lessonClientCreateDto.ClientId);
                
                if (queueOfClientUnderFirstNum is not null)
                {
                    await _queueService.DeleteClientFromQueueAsync(new LessonClientCreateDto()
                    {
                        ClientId = queueOfClientUnderFirstNum.ClientId,
                        LessonId = lesson.Id

                    }, cancellationToken);
                    lesson.Clients.Add(clientUnderFirstNum);
                    _lessonRepository.Update(lesson);
                }
                
                if (clientOnLesson != null)
                { 
                    lesson.Clients.Remove(clientOnLesson);
                    _lessonRepository.Update(lesson);
                }
                await _lessonRepository.SaveChangesAsync(cancellationToken);
            }
        }

    public async Task<bool> IsClientAtLessonAsync(LessonClientCreateDto dto, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetByIdAsync(dto.ClientId, cancellationToken);
        if (client == null)
        {
            throw new ClientDoesNotExistException("Client with such id doesn't exists");
        }

        var lesson = await _lessonRepository.GetByIdAsync(dto.LessonId, cancellationToken,
            lesson => lesson.Clients, lesson => lesson.Queues);
        if (lesson == null)
        {
            throw new LessonDoesNotExistException("Lesson with such id doesn't exists");
        }
        
        if (lesson.Clients.Contains(client)) return true;
        return false;
    }
    
    
    

}