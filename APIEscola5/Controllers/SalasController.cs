using APIEscola5.DTOs;
using APIEscola5.Models;
using APIEscola5.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace APIEscola5.Controllers;

[Route("[controller]")]
[ApiController]
public class SalasController : ControllerBase
{
    private readonly IUnityOfWork _uof;
    private readonly IMapper _mapper;
    private readonly ILogger<SalasController> _logger;

    public SalasController(IUnityOfWork uof, 
                            IMapper mapper,
                            ILogger<SalasController> logger)
    {
        _uof = uof;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SalaDTO>>> Get()
    {
        var salas = await _uof.SalaRepository.GetAllAsync();
        if(salas is null)
        {
            return NotFound("Salas não encontradas na base!");
        }

        var salasDto = _mapper.Map<IEnumerable<SalaDTO>>(salas);
        
        return Ok(salasDto);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<SalaDTO>> Get(int id)
    {
        var sala = await _uof.SalaRepository.GetAsync(id);
        if(sala is null)
        {
            return NotFound($"Sala de id = {id} não encontrada!");
        }

        var salaDto = _mapper.Map<SalaDTO>(sala);

        return Ok(salaDto);
    }

    [HttpPost]
    public async Task<ActionResult<SalaDTO>> Post(SalaDTO salaDto)
    {
        if(salaDto is null)
        {
            return BadRequest("Sala não recebida na requisição!");
        }

        var sala = _mapper.Map<Sala>(salaDto);
        _uof.SalaRepository.Create(sala);
        await _uof.CommitAsync();
        salaDto = _mapper.Map<SalaDTO>(sala);
        _logger.LogInformation($"Sala de id = {salaDto.SalaId} criada!");

        return Ok(salaDto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<SalaDTO>> Put(int id, SalaDTO salaDto)
    {
        if(salaDto.SalaId != id)
        {
            return BadRequest($"Sala com divergência de id na requisição!");
        }

        var sala = _mapper.Map<Sala>(salaDto);
        _uof.SalaRepository.Update(sala);
        await _uof.CommitAsync();
        _logger.LogInformation($"Sala de id = {id} atualizada!");

        return Ok(salaDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<SalaDTO>> Delete(int id)
    {
        var sala = await _uof.SalaRepository.GetAsync(id);
        if(sala is null)
        {
            return NotFound($"Salas de id = {id} não encontrada!");
        }

        _uof.SalaRepository.Delete(sala);
        await _uof.CommitAsync();
        var salaDto = _mapper.Map<SalaDTO>(sala);
        _logger.LogInformation($"Sala de id = {id} excluída!");

        return Ok(salaDto);
    }
}
