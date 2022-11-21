using Microsoft.AspNetCore.Mvc;
using StarDance.API.Infrastructure;
using StarDance.BLL.Interfaces;
using StarDance.Common.Dtos.AuthDtos;
using StarDance.Common.Dtos.ClientDtos;

namespace StarDance.API.Controllers;

[Route("api"), ApiController]
public class AuthController : ControllerBase
    {
    private readonly IClientService _clientService;
    private readonly JwtService _jwtService;
    public AuthController(IClientService service, JwtService jwtService)
    {
        _clientService = service;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(ClientRegisterDto clientRegisterDto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        clientRegisterDto.Password = BCrypt.Net.BCrypt.HashPassword(clientRegisterDto.Password);
        var client = await _clientService.CreateClientAsync(clientRegisterDto, cancellationToken);
        var jwt = _jwtService.Generate(client.Login, client.Role);

        var response = new AuthReadDto
        {
            AccessToken = jwt,
            Id = client.Id,
            Role = client.Role
        };
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(ClientLogintDto clientLogintDto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var client = await _clientService.GetByEmailAsync(clientLogintDto.Email, cancellationToken);
        if (client == null || !BCrypt.Net.BCrypt.Verify(clientLogintDto.Password, client.Password))
        {
            return BadRequest(new { message = "Invalid Email Or Password" });
        }
        var jwt = _jwtService.Generate(client.Login, client.Role);

        var response = new AuthReadDto
        {
            AccessToken = jwt,
            Id = client.Id,
            Role = client.Role
        };

        return Ok(response);
    }
}

