using System.ComponentModel.DataAnnotations;

namespace StarDance.Common.Dtos.ClientDtos;

public class ClientPartialUpdateDto
{
    [StringLength(15, MinimumLength = 2, ErrorMessage = "Name is too short or too long")]
    public string Name { get; set; }

    [StringLength(20, MinimumLength = 2, ErrorMessage = "Surname is too short or too long")]
    public string Surname { get; set; }

    [Phone]
    [StringLength(12, ErrorMessage = "Phone should contain 11 digits and 1 plus sign")]
    public string Phone { get; set; }

    [StringLength(25, MinimumLength = 5, ErrorMessage = "Email is too short or too long")]
    public string Email { get; set; }

    [StringLength(15, MinimumLength = 5, ErrorMessage = "Login should contain from 5 to 15 characters")]
    public string Login { get; set; }

    [StringLength(15, MinimumLength = 6, ErrorMessage = "Password should contain from 6 to 15 characters")]
    public string Password { get; set; }
}