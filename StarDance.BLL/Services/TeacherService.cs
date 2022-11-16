using AutoMapper;
using StarDance.BLL.Exceptions;
using StarDance.BLL.Interfaces;
using StarDance.Common.Dtos.ClientDtos;
using StarDance.Common.Dtos.TeacherDtos;
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
    
    public async Task<List<TeacherReadDto>> GetAllTeachersAsync()
    {
        var teachers = await _repository.GetAllAsync(
            teacher => teacher.Lessons,
            teacher => teacher.DanceType);
            var teacherReadDtos = _mapper.Map<List<TeacherReadDto>>(teachers);
        return teacherReadDtos;
    }

    public async Task<TeacherReadDto> GetTeacherByIdAsync(int id)
    {
        var teacher = await _repository.GetByIdAsync(id);
        var teacherDto = _mapper.Map<TeacherReadDto>(teacher);
        return teacherDto;
    }

    public async Task<TeacherReadDto> GetTeacherByDancetypeAsync(string dancetype)
    {
        var teacher = await _repository.FindAsync(teacher => teacher.DanceType.Name == dancetype);
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

    public async Task<TeacherReadDto> UpdateTeacherDetailsAsync(int id, TeacherPartialUpdateDto dto, CancellationToken cancellationToken)
    {
        var teacherModel = await _repository.GetByIdAsync(id);
        if (teacherModel == null)
        {
            throw new TeacherDoesNotExistException("Teacher doesn't exists");
        }

        if (dto.UserId.HasValue)
            teacherModel.UserId = dto.UserId;

        if (dto.DanceTypeId.HasValue)
            teacherModel.DanceTypeId = dto.DanceTypeId;
        
        await _repository.UpdateAsync(teacherModel, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        var teacherReadDto = _mapper.Map<TeacherReadDto>(teacherModel);
        return teacherReadDto;
    }

    public async Task<TeacherReadDto> UpdateTeacherAsync(int id, TeacherUpdateDto dto, CancellationToken cancellationToken)
    {
        var teacherModel = await _repository.GetByIdAsync(id);
        if (teacherModel == null)
        {
            throw new TeacherDoesNotExistException("Teacher doesn't exists");
        }
        
        _mapper.Map(dto,teacherModel);
        await _repository.UpdateAsync(teacherModel, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        var teacherReadDto = _mapper.Map<TeacherReadDto>(teacherModel);
        return teacherReadDto;
    }

    public async Task<bool> DeleteTeacherAsync(int id, CancellationToken cancellationToken)
    {
        var teacher = await _repository.GetByIdAsync(id);
        if (teacher != null)
        {
            await _repository.DeleteAsync(teacher, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
            return true;
        }
        return false;
    }
}