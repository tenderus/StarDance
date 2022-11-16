using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using StarDance.BLL.Interfaces;
using StarDance.Common.Dtos.ClientDtos;
using StarDance.Common.Dtos.LessonDtos;

namespace StarDance.API.Controllers;

[Route("api/clients")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService  _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clientReadDtos = await _clientService.GetAllClientsAsync();

            return Ok(clientReadDtos);
        }
        
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var clientReadDto = await _clientService.GetByIdAsync(id);
            if (clientReadDto != null)
            {
                return Ok(clientReadDto);
            }
            return NotFound();
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var clientReadDto = await _clientService.GetByEmailAsync(email);
            if (clientReadDto != null)
            {
                return Ok(clientReadDto);
            }
            return NotFound();
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] ClientRegisterDto clientRegisterDto, CancellationToken cancellationToken)
        {
            var clientReadDto = await _clientService.CreateClientAsync(clientRegisterDto, cancellationToken);
            return Ok(clientReadDto);
        }
        
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id, CancellationToken cancellationToken)
        {
            var isDeleted = await _clientService.DeleteClientAsync(id, cancellationToken);

            return isDeleted ? NoContent() : NotFound();
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] ClientUpdateDto clientUpdateDto, CancellationToken cancellationToken)
        {
            var clientReadDto = await _clientService.UpdateClientAsync(id, clientUpdateDto, cancellationToken);
            return Ok(clientReadDto);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PartialUpdateClient(int id, [FromBody] ClientPartialUpdateDto clientPartialUpdateDto, CancellationToken cancellationToken)
        {
            var clientReadDto = await _clientService.UpdateClientDetailsAsync(id, clientPartialUpdateDto, cancellationToken);
            return Ok(clientReadDto);
        }
        
        
}