using AutoMapper;
using StarDance.BLL.Exceptions;
using StarDance.BLL.Interfaces;
using StarDance.Common.Dtos.ClientDtos;
using StarDance.Common.Dtos.LessonDtos;
using StarDance.Common.Dtos.TeacherDtos;
using StarDance.Common.Helpers;
using StarDance.DAL.Interfaces;
using StarDance.Domain;

namespace StarDance.BLL.Services;

public class TeacherService : ITeacherService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Teacher> _repository;

    public TeacherService(IRepository<Teacher> repo, IMapper mapper)
    {
        _repository = repo;
        _mapper = mapper;
    }
    
    public async Task<List<TeacherReadDto>> GetAllTeachersAsync(CancellationToken cancellationToken)
    {
        var teachers = await _repository.GetAllAsync(cancellationToken,
            teacher => teacher.Lessons,
            teacher => teacher.DanceType);
            var teacherReadDtos = _mapper.Map<List<TeacherReadDto>>(teachers);
        return teacherReadDtos;
    }
    
    public async Task<PaginatedResult<TeacherReadDto>> GetPagedResult(PagedRequest pagedRequest, CancellationToken cancellationToken)
    {
        var teachersList = await _repository.GetPagedDataWithInclude(pagedRequest, cancellationToken,
            x=> x.User,
            x => x.DanceType);
        var teachersListDtos = _mapper.Map<PaginatedResult<TeacherReadDto>>(teachersList);
        return teachersListDtos;
    }

    public async Task<TeacherReadDto> GetTeacherByIdAsync(int id, CancellationToken cancellationToken)
    {
        var teacher = await _repository.GetByIdAsync(id, cancellationToken,
            teacher => teacher.User,
            teacher => teacher.DanceType,
            teacher => teacher.Lessons);
        var teacherDto = _mapper.Map<TeacherReadDto>(teacher);
        return teacherDto;
    }

    public async Task<TeacherReadDto> GetTeacherByDancetypeAsync(string dancetype, CancellationToken cancellationToken)
    {
        var teacher = await _repository.FindAsync(teacher => teacher.DanceType.Name == dancetype, cancellationToken);
        var teacherDto = _mapper.Map<TeacherReadDto>(teacher);
        return teacherDto;
    }

    public async Task<TeacherReadDto> AddTeacherAsync(TeacherUpdateDto dto, CancellationToken cancellationToken)
    {
        var teacher = _mapper.Map<Teacher>(dto);
        await _repository.AddAsync(teacher, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
        return _mapper.Map<TeacherReadDto>(teacher);
    }
    
    public async Task<TeacherReadDto> UpdateTeacherAsync(int id, TeacherUpdateDto dto, CancellationToken cancellationToken)
    {
        var teacherModel = await _repository.GetByIdAsync(id, cancellationToken);
        if (teacherModel == null)
        {
            throw new TeacherDoesNotExistException("Teacher doesn't exists");
        }
        
        _mapper.Map(dto,teacherModel);
        _repository.Update(teacherModel);
        await _repository.SaveChangesAsync(cancellationToken);

        var teacherReadDto = _mapper.Map<TeacherReadDto>(teacherModel);
        return teacherReadDto;
    }

    public async Task DeleteTeacherAsync(int id, CancellationToken cancellationToken)
    {
        var teacher = await _repository.GetByIdAsync(id, cancellationToken);
        if (teacher != null)
        {
            _repository.Delete(teacher);
            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}