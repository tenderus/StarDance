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
        public async Task<List<ClientReadDto>> GetAll(CancellationToken cancellationToken)
        {
            var clientReadDtos = await _clientService.GetAllClientsAsync(cancellationToken);

            return clientReadDtos;
        }
        
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ClientReadDto> GetById(int id, CancellationToken cancellationToken)
        {
            var clientReadDto = await _clientService.GetByIdAsync(id, cancellationToken);
            return clientReadDto;
        }

        [HttpGet("{email}")]
        public async Task<ClientReadDto> GetByEmail(string email, CancellationToken cancellationToken)
        {
            var clientReadDto = await _clientService.GetByEmailAsync(email, cancellationToken);
            return clientReadDto;
        }
        
        [HttpPost]
        public async Task<ClientReadDto> CreateClient([FromBody] ClientRegisterDto clientRegisterDto, CancellationToken cancellationToken)
        {
            var clientReadDto = await _clientService.CreateClientAsync(clientRegisterDto, cancellationToken);
            return clientReadDto;
        }
        
        
        [HttpDelete("{id}")]
        public async Task<bool> DeleteClient(int id, CancellationToken cancellationToken)
        {
            var isDeleted = await _clientService.DeleteClientAsync(id, cancellationToken);

            return isDeleted;
        }
        
        [HttpPut("{id}")]
        public async Task<ClientReadDto> UpdateClient(int id, ClientUpdateDto clientUpdateDto, CancellationToken cancellationToken)
        {
            var clientReadDto = await _clientService.UpdateClientAsync(id, clientUpdateDto, cancellationToken);
            return clientReadDto;
        }
        
        
}