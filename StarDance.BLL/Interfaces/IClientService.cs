using StarDance.Common.Dtos.ClientDtos;

namespace StarDance.BLL.Interfaces;

public interface IClientService
{
    Task<ClientReadDto> CreateClientAsync(ClientRegisterDto clientRegisterDto, CancellationToken cancellationToken);
    Task<List<ClientReadDto>> GetAllClientsAsync();
    Task<ClientReadDto> GetByEmailAsync(string email);
    Task<ClientReadDto> GetByIdAsync(int id);
    Task<ClientReadDto> UpdateClientAsync(int id, ClientUpdateDto dto, CancellationToken cancellationToken);
    Task<ClientReadDto> UpdateClientDetailsAsync(int id, ClientPartialUpdateDto dto, CancellationToken cancellationToken);
    Task<bool> DeleteClientAsync(int id, CancellationToken cancellationToken);
    
}