using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StarDance.BLL.Interfaces;
using StarDance.Common.Dtos.LessonDtos;
using StarDance.Common.Helpers;

namespace StarDance.API.Controllers;


    [Route("api/lessons")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService  _lessonService;

        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lessonReadDtos = await _lessonService.GetAllLessonsAsync();

            return Ok(lessonReadDtos);

        }
        
        [HttpGet("paginated")]
        public async Task<IActionResult> GetPaginatedLessons(PagedRequest pagedRequest)
        {
            var lessonReadDtos = await _lessonService.GetPagedResult(pagedRequest);
            return Ok(lessonReadDtos);
        }
        
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var lessonReadDto = await _lessonService.GetLessonByIdAsync(id);
            if (lessonReadDto != null)
            {
                return Ok(lessonReadDto);
            }
            return NotFound();
        }

        [HttpGet("{dancetype}")]
        public async Task<IActionResult> GetByDancetype(string dancetype)
        {
            var lessonReadDto = await _lessonService.GetLessonByDancetypeAsync(dancetype);
            if (lessonReadDto != null)
            {
                return Ok(lessonReadDto);
            }
            return NotFound();
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateLesson([FromBody] LessonUpdateDto lessonUpdateDto, CancellationToken cancellationToken)
        {
            var lessonReadDto = await _lessonService.CreateLessonAsync(lessonUpdateDto, cancellationToken);
            return Ok(lessonReadDto);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLesson(int id, CancellationToken cancellationToken)
        {
            var isDeleted = await _lessonService.DeleteLessonAsync(id, cancellationToken);

            return isDeleted ? NoContent() : NotFound();
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLesson(int id, [FromBody] LessonUpdateDto lessonUpdateDto, CancellationToken cancellationToken)
        {
            var lessonReadDto = await _lessonService.UpdateLessonAsync(id, lessonUpdateDto, cancellationToken);
            return Ok(lessonReadDto);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PartialUpdateLesson(int id, [FromBody] LessonPartialUpdateDto lessonPartialUpdateDto, CancellationToken cancellationToken)
        {
            var lessonReadDto = await _lessonService.UpdateLessonDetailsAsync(id, lessonPartialUpdateDto, cancellationToken);
            return Ok(lessonReadDto);
        }
        
        [HttpPost("clientsLessons")]
        public async Task<IActionResult> AddClientToLesson([FromBody] LessonClientCreateDto lessonClientCreateDto, CancellationToken cancellationToken)
        {
            var lessonReadDto = await _lessonService.AddClientToLessonAsync(lessonClientCreateDto, cancellationToken);
            return Ok(lessonReadDto);
        }
        
        
        [HttpDelete("clientLesson")]
        public async Task<IActionResult> DeleteClientFromLesson([FromBody] LessonClientCreateDto lessonClientCreateDto, CancellationToken cancellationToken)
        {
            await _lessonService.DeleteClientFromLessonAsync(lessonClientCreateDto, cancellationToken);
            return Ok();
        }
        
        [HttpGet("lessonsOfClient/{id}")]
        public async Task<IActionResult> GetLessonsByClient(int id)
        {
            var lessonReadDto = await _lessonService.GetLessonsByClientId(id);
            if (lessonReadDto != null)
            {
                return Ok(lessonReadDto);
            }
            return NotFound();
        }
        
        
        [HttpPost("clientIsAtLesson")]
        public async Task<IActionResult> CheckClientIsAtLesson([FromBody] LessonClientCreateDto lessonClientCreateDto)
        {
            var result = await _lessonService.CheckClientIsAtLessonAsync(lessonClientCreateDto);
            return Ok(result);
        }
        


    }