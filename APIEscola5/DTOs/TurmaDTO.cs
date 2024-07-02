using System.ComponentModel.DataAnnotations;

namespace APIEscola5.DTOs;

public class TurmaDTO
{
    public int TurmaId { get; set; }

    [Required]
    [StringLength(12)]
    public string? Ano { get; set; }

    [Required]
    [StringLength(1)]
    public string? Sequencia { get; set; }

    public int? SalaId { get; set; }
}
