using System.ComponentModel.DataAnnotations;

namespace APIEscola5.DTOs;

public class AlunoDTO
{
    public int AlunoId { get; set; }

    [Required]
    [StringLength(50)]
    public string? Nome { get; set; }

    [Required]
    [StringLength(12)]
    public string? Identidade { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime? Nascimento { get; set; }

    public int? TurmaId { get; set; }
}
