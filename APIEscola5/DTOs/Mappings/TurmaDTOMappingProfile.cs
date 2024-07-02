using APIEscola5.Models;
using AutoMapper;

namespace APIEscola5.DTOs.Mappings;

public class TurmaDTOMappingProfile : Profile
{
    public TurmaDTOMappingProfile()
    {
        CreateMap<Turma, TurmaDTO>().ReverseMap();
    }
}
