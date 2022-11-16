using Microsoft.AspNetCore.Mvc;
using StarDance.API.Infrastructure;
using StarDance.BLL.Interfaces;
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
        clientRegisterDto.Password = BCrypt.Net.BCrypt.HashPassword(clientRegisterDto.Password);
        await _clientService.CreateClientAsync(clientRegisterDto, cancellationToken);
        var client = await _clientService.GetByEmailAsync(clientRegisterDto.Email);
        var jwt = _jwtService.Generate(client.Login, client.Role);

        var response = new
        {
            accessToken = jwt,
            id = client.Id,
            role= client.Role
        };
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(ClientLogintDto clientLogintDto)
    {
        var client = await _clientService.GetByEmailAsync(clientLogintDto.Email);
        if (client == null || !BCrypt.Net.BCrypt.Verify(clientLogintDto.Password, client.Password))
        {
            return BadRequest(new { message = "Invalid Email Or Password" });
        }
        var jwt = _jwtService.Generate(client.Login, client.Role);

        var response = new
        {
            accessToken = jwt,
            id = client.Id,
            role = client.Role
        };

        return Ok(response);
    }
}

