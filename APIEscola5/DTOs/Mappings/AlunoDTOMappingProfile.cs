using APIEscola5.Models;
using AutoMapper;

namespace APIEscola5.DTOs.Mappings;

public class AlunoDTOMappingProfile : Profile
{
    public AlunoDTOMappingProfile()
    {
        CreateMap<Aluno, AlunoDTO>().ReverseMap();
    }
}
