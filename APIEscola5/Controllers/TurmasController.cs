using APIEscola5.DTOs;
using APIEscola5.Models;
using APIEscola5.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace APIEscola5.Controllers;

[Route("[controller]")]
[ApiController]
public class TurmasController : ControllerBase
{
    private readonly IUnityOfWork _uof;
    private readonly IMapper _mapper;
    private readonly ILogger<TurmasController> _logger;

    public TurmasController(IUnityOfWork uof,
                            IMapper mapper,
                            ILogger<TurmasController> logger)
    {
        _uof = uof;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TurmaDTO>>> Get()
    {
        var turmas = await _uof.TurmaRepository.GetAllAsync();
        if (turmas is null)
        {
            return NotFound("Turmas não encontradas na base!");
        }

        var turmasDto = _mapper.Map<IEnumerable<TurmaDTO>>(turmas);
        return Ok(turmasDto);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TurmaDTO>> Get(int id)
    {
        var turma = await _uof.TurmaRepository.GetAsync(id);
        if (turma is null)
        {
            return NotFound($"Turma de id = {id} não encontrada!");
        }

        var turmaDto = _mapper.Map<TurmaDTO>(turma);

        return Ok(turmaDto);
    }

    [HttpGet("Sala/{id:int}")]
    public async Task<ActionResult<IEnumerable<TurmaDTO>>> GetTurmasSala(int id)
    {
        var turmas = await _uof.TurmaRepository.GetTurmasSalaAsync(id);
        if(turmas is null)
        {
            return NotFound($"Turmas não identificadas na sala de id = {id}!");
        }

        var turmasDto = _mapper.Map<IEnumerable<TurmaDTO>>(turmas);

        return Ok(turmasDto);
    }

    [HttpGet("Nome/{id:int}")]
    public async Task<ActionResult<string>> GetNomeTurma(int id)
    {
        var nomeTurma = await _uof.TurmaRepository.GetNomeTurmaAsync(id);
        if(nomeTurma is null)
        {
            return BadRequest($"Nome da turma de id = {id} inexistente!");
        }

        return Ok(nomeTurma);
    }

    [HttpPost]
    public async Task<ActionResult<TurmaDTO>> Post(TurmaDTO turmaDto)
    {
        if (turmaDto is null)
        {
            return BadRequest("Turma não recebida na requisição!");
        }

        var turma = _mapper.Map<Turma>(turmaDto);
        _uof.TurmaRepository.Create(turma);
        await _uof.CommitAsync();
        turmaDto = _mapper.Map<TurmaDTO>(turma);
        _logger.LogInformation($"Turma de id = {turmaDto.TurmaId} criada!");

        return Ok(turmaDto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<TurmaDTO>> Put(int id, TurmaDTO turmaDto)
    {
        if (turmaDto.TurmaId != id)
        {
            return BadRequest($"Turma com divergência de id na requisição!");
        }

        var turma = _mapper.Map<Turma>(turmaDto);
        _uof.TurmaRepository.Update(turma);
        await _uof.CommitAsync();
        _logger.LogInformation($"Turma de id = {id} atualizada!");

        return Ok(turmaDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<TurmaDTO>> Delete(int id)
    {
        var turma = await _uof.TurmaRepository.GetAsync(id);
        if(turma is null)
        {
            return NotFound($"Turma de id = {id} não encontrada!");
        }

        _uof.TurmaRepository.Delete(turma);
        await _uof.CommitAsync();
        var turmaDto = _mapper.Map<TurmaDTO>(turma);
        _logger.LogInformation($"Turma de id = {id} excluída!");

        return Ok(turmaDto);
    }
}
