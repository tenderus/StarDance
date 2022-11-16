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
    private readonly IRepository<Lesson> _lessonRepository;

    public ClientService(IRepository<User> repo, IRepository<Lesson> lessonRepository, IMapper mapper)
    {
        _repository = repo;
        _lessonRepository = lessonRepository;
        _mapper = mapper;
    }

    public async Task<ClientReadDto> CreateClientAsync(ClientRegisterDto clientDto, CancellationToken cancellationToken)
    {
        var client = _mapper.Map<User>(clientDto);
        await _repository.AddAsync(client, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
        return _mapper.Map<ClientReadDto>(client);
    }

    public async Task<List<ClientReadDto>> GetAllClientsAsync()
    {
        var clients =  await _repository.GetAllAsync( 
            client => client.Lessons,
            client => client.Queues,
            client => client.Absences);
        var clientReadDtos = _mapper.Map<List<ClientReadDto>>(clients);
        
        return clientReadDtos;
    }
    
    public async Task<ClientReadDto> GetByEmailAsync(string email)
    {
        var client = await _repository.FindAsync(client => client.Email == email);
        var clientDto = _mapper.Map<ClientReadDto>(client);
        return clientDto;
    }

    public async Task<ClientReadDto> GetByIdAsync(int id)
    {
        var client = await _repository.GetByIdAsync(id, client => client.Lessons);
        var clientDto = _mapper.Map<ClientReadDto>(client);
        return clientDto;
    }
    

    public async Task<ClientReadDto> UpdateClientAsync(int id, ClientUpdateDto dto, CancellationToken cancellationToken)
    {
        var clientModel = await _repository.GetByIdAsync(id);
        if (clientModel == null)
        {
            throw new ClientDoesNotExistException("Client doesn't exists");
        }
        
        _mapper.Map(dto,clientModel);
        await _repository.UpdateAsync(clientModel, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        var clientReadDto = _mapper.Map<ClientReadDto>(clientModel);
        return clientReadDto;
    }

    public async Task<ClientReadDto> UpdateClientDetailsAsync(int id, ClientPartialUpdateDto dto, CancellationToken cancellationToken)
    {
        var clientModel = await _repository.GetByIdAsync(id);
        if (clientModel == null)
        {
            throw new Exception("Client doesn't exists");
        }

        if (!string.IsNullOrWhiteSpace(dto.Name))
            clientModel.Name = dto.Name;

        if (!string.IsNullOrWhiteSpace(dto.Surname))
            clientModel.Surname = dto.Surname;

        if (!string.IsNullOrWhiteSpace(dto.Phone))
            clientModel.Phone = dto.Phone;
        
        if (!string.IsNullOrWhiteSpace(dto.Email))
            clientModel.Email = dto.Email;
            
        if (!string.IsNullOrWhiteSpace(dto.Login))
            clientModel.Login = dto.Login;
        
        if (!string.IsNullOrWhiteSpace(dto.Password))
            clientModel.Password = dto.Password;

        await _repository.UpdateAsync(clientModel, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
        var clientReadDto = _mapper.Map<ClientReadDto>(clientModel);
        return clientReadDto;
    }
    
    public async Task<bool> DeleteClientAsync(int id, CancellationToken cancellationToken)
    {
        var client = await _repository.GetByIdAsync(id);
        if (client != null)
        {
            await _repository.DeleteAsync(client, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
            return true;
        }
        return false;
    }
}