using System.ComponentModel.DataAnnotations;

namespace StarDance.Common.Dtos.RoomDtos;

public class RoomPartialUpdateDto
{
    [Range(1, 5)] public int Number { get; set; }

    [Range(1, 20)] public int Capacity { get; set; }
}