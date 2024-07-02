using APIEscola5.Models;
using AutoMapper;

namespace APIEscola5.DTOs.Mappings;

public class SalaDTOMappingProfile : Profile
{
    public SalaDTOMappingProfile()
    {
        CreateMap<Sala, SalaDTO>().ReverseMap();
    }
}
