using System.Security.Cryptography;
using AutoMapper;
using StarDance.BLL.Exceptions;
using StarDance.BLL.Interfaces;
using StarDance.Common.Dtos.ClientDtos;
using StarDance.DAL.Interfaces;
using StarDance.Domain;

namespace StarDance.BLL.Services;

public class ClientService : IClientService
{
    private readonly IMapper _mapper;
    private readonly IRepository<User> _repository;

    public ClientService(IRepository<User> repo, IMapper mapper)
    {
        _repository = repo;
        _mapper = mapper;
    }

    public async Task<ClientReadDto> CreateClientAsync(ClientRegisterDto clientDto, CancellationToken cancellationToken)
    {
        var client = _mapper.Map<User>(clientDto);
        await _repository.AddAsync(client, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
        return _mapper.Map<ClientReadDto>(client);
    }

    public async Task<List<ClientReadDto>> GetAllClientsAsync(CancellationToken cancellationToken)
    {
        var clients =  await _repository.GetAllAsync( cancellationToken,
            client => client.Lessons,
            client => client.Queues);
        var clientReadDtos = _mapper.Map<List<ClientReadDto>>(clients);
        
        return clientReadDtos;
    }
    
    public async Task<ClientReadDto> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var client = await _repository.FindAsync(client => client.Email == email, cancellationToken);
        var clientDto = _mapper.Map<ClientReadDto>(client);
        return clientDto;
    }

    public async Task<ClientReadDto> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var client = await _repository.GetByIdAsync(id, cancellationToken, client => client.Lessons);
        var clientDto = _mapper.Map<ClientReadDto>(client);
        return clientDto;
    }
    

    public async Task<ClientReadDto> UpdateClientAsync(int id, ClientUpdateDto dto, CancellationToken cancellationToken)
    {
        var clientModel = await _repository.GetByIdAsync(id, cancellationToken);
        if (clientModel == null)
        {
            throw new ClientDoesNotExistException("Client doesn't exists");
        }
        
        _mapper.Map(dto,clientModel);
         _repository.Update(clientModel);
        await _repository.SaveChangesAsync(cancellationToken);

        var clientReadDto = _mapper.Map<ClientReadDto>(clientModel);
        return clientReadDto;
    }
    
    public async Task<bool> DeleteClientAsync(int id, CancellationToken cancellationToken)
    {
        var client = await _repository.GetByIdAsync(id, cancellationToken);
        if (client != null)
        {
            _repository.Delete(client);
            await _repository.SaveChangesAsync(cancellationToken);
            return true;
        }
        return false;
    }
}