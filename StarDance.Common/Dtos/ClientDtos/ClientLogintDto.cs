using System.ComponentModel.DataAnnotations;

namespace StarDance.Common.Dtos.ClientDtos;

public class ClientLogintDto
{
    public string Email { get; set; }

    [StringLength(15, MinimumLength = 6, ErrorMessage = "Password should contain from 6 to 15 characters")]
    public string Password { get; set; }
}