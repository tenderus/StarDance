using StarDance.Common.Dtos.ClientDtos;

namespace StarDance.BLL.Interfaces;

public interface IClientService
{
    Task<ClientReadDto> CreateClientAsync(ClientRegisterDto clientRegisterDto, CancellationToken cancellationToken);
    Task<List<ClientReadDto>> GetAllClientsAsync(CancellationToken cancellationToken);
    Task<ClientReadDto> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<ClientReadDto> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<ClientReadDto> UpdateClientAsync(int id, ClientUpdateDto dto, CancellationToken cancellationToken);
    Task<bool> DeleteClientAsync(int id, CancellationToken cancellationToken);
    
}