using System.ComponentModel.DataAnnotations;
using StarDance.Common.Dtos.DanceTypeDtos;

namespace StarDance.Common.Dtos.TeacherDtos;

public class TeacherUpdateDto
{
    public int UserId { get; set; }

    public int DanceTypeId{ get; set; }
}