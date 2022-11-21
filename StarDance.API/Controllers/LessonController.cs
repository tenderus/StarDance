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
        public async Task<List<LessonReadDto>> GetAll(CancellationToken cancellationToken)
        {
            var lessonReadDtos = await _lessonService.GetAllLessonsAsync(cancellationToken);

            return lessonReadDtos;

        }
        
        [HttpPost("paginated")]
        public async Task<PaginatedResult<LessonReadDto>> GetPaginatedLessons(PagedRequest pagedRequest, CancellationToken cancellationToken)
        {
            var lessonReadDtos = await _lessonService.GetPagedResult(pagedRequest, cancellationToken);
            return lessonReadDtos;
        }
        
        [HttpGet]
        [Route("{id:int}")]
        public async Task<LessonReadDto> GetById(int id, CancellationToken cancellationToken)
        {
            var lessonReadDto = await _lessonService.GetLessonByIdAsync(id, cancellationToken);
            return lessonReadDto;
        }

        [HttpGet("{dancetype}")]
        public async Task<LessonReadDto> GetByDancetype(string dancetype, CancellationToken cancellationToken)
        {
            var lessonReadDto = await _lessonService.GetLessonByDancetypeAsync(dancetype, cancellationToken);
            return lessonReadDto;
        }
        
        [HttpPost]
        public async Task<LessonReadDto> CreateLesson([FromBody] LessonUpdateDto lessonUpdateDto, CancellationToken cancellationToken)
        {
            var lessonReadDto = await _lessonService.CreateLessonAsync(lessonUpdateDto, cancellationToken);
            return lessonReadDto;
        }
        
        [HttpDelete("{id}")]
        public async Task<bool> DeleteLesson(int id, CancellationToken cancellationToken)
        {
            var isDeleted = await _lessonService.DeleteLessonAsync(id, cancellationToken);

            return isDeleted;
        }
        
        [HttpPut("{id}")]
        public async Task<LessonReadDto> UpdateLesson(int id, LessonUpdateDto lessonUpdateDto, CancellationToken cancellationToken)
        {
            var lessonReadDto = await _lessonService.UpdateLessonAsync(id, lessonUpdateDto, cancellationToken);
            return lessonReadDto;
        }
        
        [HttpPost("clientsLessons")]
        public async Task<LessonReadDto> AddClientToLesson(LessonClientCreateDto lessonClientCreateDto, CancellationToken cancellationToken)
        {
            var lessonReadDto = await _lessonService.AddClientToLessonAsync(lessonClientCreateDto, cancellationToken);
            return lessonReadDto;
        }
        
        
        [HttpDelete("clientLesson")]
        public async Task DeleteClientFromLesson(LessonClientCreateDto lessonClientCreateDto, CancellationToken cancellationToken)
        { 
            await _lessonService.DeleteClientFromLessonAsync(lessonClientCreateDto, cancellationToken);
        }
        
        [HttpGet("lessonsOfClient/{id}")]
        public async Task<List<LessonReadDto>> GetLessonsByClient(int id, CancellationToken cancellationToken)
        {
            var lessonReadDto = await _lessonService.GetLessonsByClientId(id, cancellationToken);
            return lessonReadDto;
        }
        
        
        [HttpPost("clientIsAtLesson")]
        public async Task<bool> IsClientAtLesson(LessonClientCreateDto lessonClientCreateDto, CancellationToken cancellationToken)
        {
            var result = await _lessonService.IsClientAtLessonAsync(lessonClientCreateDto, cancellationToken);
            return result;
        }
        


    }