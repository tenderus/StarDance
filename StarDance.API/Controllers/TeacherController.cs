using Microsoft.AspNetCore.Mvc;
using StarDance.BLL.Interfaces;
using StarDance.Common.Dtos.ClientDtos;
using StarDance.Common.Dtos.TeacherDtos;

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
    public async Task<IActionResult> GetAllTeachers()
    {
        var teacherReadDtos = await _teacherService.GetAllTeachersAsync();

        return Ok(teacherReadDtos);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetTeacherById(int id)
    {
        var teacherReadDto = await _teacherService.GetTeacherByIdAsync(id);
        if (teacherReadDto != null)
        {
            return Ok(teacherReadDto);
        }
    
        return NotFound();
    }
    
    [HttpGet("{dancetype}")]
    public async Task<IActionResult> GetTeacherByDancetype(string dancetype)
    {
        var teacherReadDto = await _teacherService.GetTeacherByDancetypeAsync(dancetype);
        if (teacherReadDto != null)
        {
            return Ok(teacherReadDto);
        }
    
        return NotFound();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTeacher([FromBody] TeacherUpdateDto teacherUpdateDto, CancellationToken cancellationToken)
    {
        var teacherReadDto = await _teacherService.AddTeacherAsync(teacherUpdateDto, cancellationToken);
        return Ok(teacherReadDto);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTeacher(int id, CancellationToken cancellationToken)
    {
        var isDeleted = await _teacherService.DeleteTeacherAsync(id, cancellationToken);
    
        return isDeleted ? NoContent() : NotFound();
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTeacher(int id, [FromBody] TeacherUpdateDto teacherUpdateDto, CancellationToken cancellationToken)
    {
        var teacherReadDto = await _teacherService.UpdateTeacherAsync(id, teacherUpdateDto, cancellationToken);
        return Ok(teacherReadDto);
    }
    
    [HttpPatch("{id}")]
    public async Task<IActionResult> PartialUpdateTeacher(int id,
        [FromBody] TeacherPartialUpdateDto teacherPartialUpdateDto, CancellationToken cancellationToken)
    {
        var teacherReadDto = await _teacherService.UpdateTeacherDetailsAsync(id, teacherPartialUpdateDto, cancellationToken);
        return Ok(teacherReadDto);
    }

}  
        
