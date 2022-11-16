using System.Globalization;
using AutoMapper;
using StarDance.BLL.Exceptions;
using StarDance.BLL.Interfaces;
using StarDance.Common.Dtos.DanceTypeDtos;
using StarDance.Common.Dtos.LessonDtos;
using StarDance.Common.Dtos.QueueDtos;
using StarDance.Common.Helpers;
using StarDance.DAL;
using StarDance.DAL.Interfaces;
using StarDance.Domain;

namespace StarDance.BLL.Services;

public class LessonService : ILessonService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Lesson> _repository;
    private readonly IRepository<Room> _roomRepository;
    private readonly IRepository<User> _clientRepository;
    private readonly ILessonRepository _lessonRepository;
    private readonly IRepository<Lesson> _generalLessonRepository;
    private readonly IRepository<Queue> _queueRepository;

    public LessonService(IRepository<Lesson> repo, IMapper mapper, IRepository<Room> roomRepository, IRepository<User> clientRepository, ILessonRepository lessonRepository, IRepository<Queue> queueRepository, IRepository<Lesson> generalLessonRepository)
    {
        _repository = repo;
        _mapper = mapper;
        _roomRepository = roomRepository;
        _lessonRepository = lessonRepository;
        _clientRepository = clientRepository;
        _queueRepository = queueRepository;
        _generalLessonRepository = generalLessonRepository;
    }

    public async Task<LessonReadDto> CreateLessonAsync(LessonUpdateDto dto, CancellationToken cancellationToken)
    {

        var lessonModel = _mapper.Map<Lesson>(dto);
        await _repository.AddAsync(lessonModel, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
        var lessonReadDto = _mapper.Map<LessonReadDto>(lessonModel);
        return lessonReadDto;
    }

    public async Task<bool> DeleteLessonAsync(int id, CancellationToken cancellationToken)
    {
        var lesson = await _repository.GetByIdAsync(id);
        if (lesson != null)
        {
            await _repository.DeleteAsync(lesson, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
            return true;
        }
        return false;
    }
    
    public async Task<List<LessonsOfClientDto>> GetLessonsByClientId(int id)
    {
        var client = await _clientRepository.GetByIdAsync(id, client => client.Lessons);
        var lessonsOfClient = await _repository.FindAllWithIncludeAsync(lesson => lesson.Clients.Contains(client),
            lesson => lesson.Teacher.User,
            lesson => lesson.Room,
            lesson => lesson.Teacher.DanceType);
        var lessonReadDtos = _mapper.Map<List<LessonsOfClientDto>>(lessonsOfClient);
        foreach (var lesson in lessonReadDtos)
        {
            // lesson.FreePlaces = lesson.RoomReadDto.Capacity;
            lesson.DanceType = lesson.TeacherReadDto.DanceTypeId switch
            {
                1 => "Hip-hop",
                2 => "Ballet",
                3 => "Salsa",
                _ => lesson.DanceType
            };
        }
        
        return lessonReadDtos;

    }
    
    public async Task<PaginatedResult<LessonReadDto>> GetPagedResult(PagedRequest pagedRequest)
    {
        var lessonsList = await _repository.GetPagedDataWithInclude(pagedRequest, x=> x.Clients, x => x.Teacher.User, x => x.Room);
        var lessonListDtos = _mapper.Map<PaginatedResult<LessonReadDto>>(lessonsList);
        return lessonListDtos;
    }
    
    public async Task<List<LessonReadDto>> GetAllLessonsAsync()
    {
        var lessons =  await _repository.GetAllAsync( 
            lesson => lesson.Teacher.User,
            lesson => lesson.Clients,
            lesson => lesson.Room,
            lesson => lesson.Queues);
        
        
        var lessonReadDtos = _mapper.Map<List<LessonReadDto>>(lessons);
        foreach (var lesson in lessonReadDtos)
        {
            lesson.FreePlaces = lesson.RoomReadDto.Capacity - lesson.Clients.Count;

            lesson.DanceType = lesson.TeacherReadDto.DanceTypeId switch
            {
                1 => "Hip-hop",
                2 => "Ballet",
                3 => "Salsa",
                _ => lesson.DanceType
            };
        }
        return lessonReadDtos;
    }
    

    public async Task<LessonReadDto> GetLessonByIdAsync(int id)
    {
        var lesson = await _repository.GetByIdAsync(id, lesson => lesson.Teacher,
            lesson => lesson.Room);
        var lessonDto = _mapper.Map<LessonReadDto>(lesson);
        return lessonDto;
    }

    public async Task<LessonReadDto> GetLessonByDancetypeAsync(string dancetype)
    {
        var lesson = await _lessonRepository.GetByDancetypeAsync(dancetype, lesson => lesson.Teacher,
            lesson => lesson.Room);
        var lessonDto = _mapper.Map<LessonReadDto>(lesson);
        return lessonDto;
    }

    public async Task<LessonReadDto> UpdateLessonDetailsAsync(int id, LessonPartialUpdateDto dto, CancellationToken cancellationToken)
    {
        var lessonModel = await _repository.GetByIdAsync(id);
        if (lessonModel == null)
        {
            throw new Exception("Lesson with such id doesn't exists");
        }

        if (dto.DateTime.HasValue)
            lessonModel.DateTime = dto.DateTime.Value;

        if (dto.TeacherId.HasValue)
            lessonModel.TeacherId = dto.TeacherId.Value;
        
        if (dto.RoomId.HasValue)
            lessonModel.RoomId = dto.RoomId.Value;
        
        await _repository.UpdateAsync(lessonModel, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        var lessonReadDto = _mapper.Map<LessonReadDto>(lessonModel);
        return lessonReadDto;
    }

    public async Task<LessonReadDto> UpdateLessonAsync(int id, LessonUpdateDto dto, CancellationToken cancellationToken)
    {
        var lessonModel = await _repository.GetByIdAsync(id);
        if (lessonModel == null)
        {
            throw new Exception("Lesson with such id doesn't exists");
        }
        
        _mapper.Map(dto,lessonModel);
        await _repository.UpdateAsync(lessonModel, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        var lessonReadDto = _mapper.Map<LessonReadDto>(lessonModel);
        return lessonReadDto;
    }

    public async Task<LessonReadDto> AddClientToLessonAsync(LessonClientCreateDto lessonClientCreateDto, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetByIdAsync(lessonClientCreateDto.ClientId);
        if (client == null)
        {
            throw new ClientDoesNotExistException("Client with such id doesn't exists");
        }
        
        var lesson = await _repository.GetByIdAsync(lessonClientCreateDto.LessonId, lesson => lesson.Clients, lesson => lesson.Queues);
        if (lesson == null)
        {
            throw new LessonDoesNotExistException("Lesson with such id doesn't exists");
        }

        var roomId = lesson.RoomId;
        var room = await _roomRepository.GetByIdAsync(roomId);

        if (room.Capacity > lesson.Clients.Count)
        {
            lesson.Clients.Add(client);
            await _repository.UpdateAsync(lesson, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
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
            var client = await _clientRepository.GetByIdAsync(lessonClientCreateDto.ClientId);
            if (client == null)
            {
                throw new ClientDoesNotExistException("Client with such id doesn't exists");
            }

            var lesson = await _repository.GetByIdAsync(lessonClientCreateDto.LessonId, lesson => lesson.Clients,
                lesson => lesson.Queues);
            if (lesson == null)
            {
                throw new LessonDoesNotExistException("Lesson with such id doesn't exists");
            }

            var firstNumInQueue = lesson.Queues.FirstOrDefault(x => x.LessonId == lessonClientCreateDto.LessonId && x.NumberOfOrder == 1);
            var clientUnderFirstNum = await _clientRepository.FindAsync(x => firstNumInQueue != null && x.Id == firstNumInQueue.ClientId);
            
            if (lesson.Clients is not null)
            {
                var clientOnLesson = lesson.Clients.FirstOrDefault(x => x.Id == lessonClientCreateDto.ClientId);
                
                if (firstNumInQueue is not null)
                {
                    lesson.Queues.Remove(firstNumInQueue);
                    await _queueRepository.DeleteAsync(firstNumInQueue, cancellationToken);
                    await _queueRepository.SaveChangesAsync(cancellationToken);
                    lesson.Clients.Add(clientUnderFirstNum);
                    await _repository.UpdateAsync(lesson, cancellationToken);
                    await _repository.SaveChangesAsync(cancellationToken);
                }
                
                if (clientOnLesson != null)
                { 
                    lesson.Clients.Remove(clientOnLesson);
                    await _repository.UpdateAsync(lesson, cancellationToken);
                    await _repository.SaveChangesAsync(cancellationToken);
                }
            }
        }

    public async Task<bool> CheckClientIsAtLessonAsync(LessonClientCreateDto dto)
    {
        var client = await _clientRepository.GetByIdAsync(dto.ClientId);
        if (client == null)
        {
            throw new ClientDoesNotExistException("Client with such id doesn't exists");
        }

        var lesson = await _repository.GetByIdAsync(dto.LessonId, lesson => lesson.Clients, lesson => lesson.Queues);
        if (lesson == null)
        {
            throw new LessonDoesNotExistException("Lesson with such id doesn't exists");
        }
        
        if (lesson.Clients.Contains(client)) return true;
        return false;
    }
    
    
    

}