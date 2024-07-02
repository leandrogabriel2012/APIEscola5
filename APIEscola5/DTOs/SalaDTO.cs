using System.ComponentModel.DataAnnotations;

namespace APIEscola5.DTOs;

public class SalaDTO
{
    public int SalaId { get; set; }

    [Required]
    [StringLength(20)]
    public string? Numero { get; set; }
}
