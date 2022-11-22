using AutoMapper;
using StarDance.BLL.Exceptions;
using StarDance.BLL.Interfaces;
using StarDance.Common.Dtos.AbsenceDtos;
using StarDance.Common.Dtos.LessonDtos;
using StarDance.DAL.Interfaces;
using StarDance.Domain;

namespace StarDance.BLL.Services;

public class AbsenceService : IAbsenceService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Absence> _repository;
    private readonly IRepository<User> _clientRepository;
    private readonly IRepository<Lesson> _lessonRepository;

    public AbsenceService(IRepository<Absence> repo, IMapper mapper, IRepository<User> clientRepository, IRepository<Lesson> lessonRepository)
    {
        _repository = repo;
        _mapper = mapper;
        _lessonRepository = lessonRepository;
        _clientRepository = clientRepository;
    }
    
    public async Task AddInAbsencesAsync(LessonClientCreateDto dto, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetByIdAsync(dto.ClientId, cancellationToken);
        if (client == null)
        {
            throw new ClientDoesNotExistException("Client with such id doesn't exists");
        }

        var lesson = await _lessonRepository.GetByIdAsync(dto.LessonId,
            cancellationToken,
            lesson => lesson.Absences);
        if (lesson == null)
        {
            throw new LessonDoesNotExistException("Lesson with such id doesn't exists");
        }

        var absenceModel = new AbsenceUpdateDto()
        {
            ClientId = dto.ClientId,
            LessonId = dto.LessonId
        };
        var absence = _mapper.Map<Absence>(absenceModel);

        lesson.Absences.Add(absence);
        await _repository.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteFromAbsencesAsync(LessonClientCreateDto dto, CancellationToken cancellationToken)
    {
        var lesson = await _lessonRepository.GetByIdAsync(dto.LessonId,
            cancellationToken,
            lesson => lesson.Absences);
        if (lesson == null)
        {
            throw new LessonDoesNotExistException("Lesson with such id doesn't exists");
        }
        
        var absence = await _repository.FindAsync(absence => absence.LessonId == dto.LessonId && absence.ClientId == dto.ClientId, cancellationToken);
        if (absence == null)
        {
            throw new AbsenceDoesNotExist("Absence with this client id and lesson id doesn't exists");
        }

        lesson.Absences.Remove(absence);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}