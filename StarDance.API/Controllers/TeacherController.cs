using Microsoft.AspNetCore.Mvc;
using StarDance.BLL.Interfaces;
using StarDance.Common.Dtos.ClientDtos;
using StarDance.Common.Dtos.TeacherDtos;
using StarDance.Common.Helpers;

namespace StarDance.API.Controllers;

[Route("api/teachers")]
[ApiController]
public class TeacherController : ControllerBase
{
    private readonly ITeacherService _teacherService;

    public TeacherController(ITeacherService teacherService)
    {
        _teacherService = teacherService;
    }

    [HttpGet]
    public async Task<List<TeacherReadDto>> GetAllTeachers(CancellationToken cancellationToken)
    {
        var teacherReadDtos = await _teacherService.GetAllTeachersAsync(cancellationToken);

        return teacherReadDtos;
    }
    
    [HttpPost("paginated")]
    public async Task<PaginatedResult<TeacherReadDto>> GetPaginatedLessons(PagedRequest pagedRequest, CancellationToken cancellationToken)
    {
        var teachersReadDtos = await _teacherService.GetPagedResult(pagedRequest, cancellationToken);
        return teachersReadDtos;
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<TeacherReadDto> GetTeacherById(int id, CancellationToken cancellationToken)
    {
        var teacherReadDto = await _teacherService.GetTeacherByIdAsync(id, cancellationToken);
        return teacherReadDto;
    }
    
    [HttpGet("{dancetype}")]
    public async Task<TeacherReadDto> GetTeacherByDancetype(string dancetype, CancellationToken cancellationToken)
    {
        var teacherReadDto = await _teacherService.GetTeacherByDancetypeAsync(dancetype, cancellationToken);
        return teacherReadDto;
    }
    
    [HttpPost]
    public async Task<TeacherReadDto> CreateTeacher(TeacherUpdateDto teacherUpdateDto, CancellationToken cancellationToken)
    {
        var teacherReadDto = await _teacherService.AddTeacherAsync(teacherUpdateDto, cancellationToken);
        return teacherReadDto;
    }
    
    [HttpDelete("{id}")]
    public async Task DeleteTeacher(int id, CancellationToken cancellationToken)
    {
        await _teacherService.DeleteTeacherAsync(id, cancellationToken);
    }
    
    [HttpPut("{id}")]
    public async Task<TeacherReadDto> UpdateTeacher(int id, [FromBody] TeacherUpdateDto teacherUpdateDto, CancellationToken cancellationToken)
    {
        var teacherReadDto = await _teacherService.UpdateTeacherAsync(id, teacherUpdateDto, cancellationToken);
        return teacherReadDto;
    }
    

}  
        
