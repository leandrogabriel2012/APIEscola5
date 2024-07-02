using APIEscola5.DTOs;
using APIEscola5.Models;
using APIEscola5.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace APIEscola5.Controllers;

[Route("[controller]")]
[ApiController]
public class AlunosController : ControllerBase
{
    private readonly IUnityOfWork _uof;
    private readonly IMapper _mapper;
    private readonly ILogger<AlunosController> _logger;

    public AlunosController(IUnityOfWork uof,
                            IMapper mapper,
                            ILogger<AlunosController> logger)
    {
        _uof = uof;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AlunoDTO>>> Get()
    {
        var alunos = await _uof.AlunoRepository.GetAllAsync();
        if (alunos is null)
        {
            return NotFound("Alunos não encontrados na base!");
        }

        var alunosDto = _mapper.Map<IEnumerable<AlunoDTO>>(alunos);
        return Ok(alunosDto);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AlunoDTO>> Get(int id)
    {
        var aluno = await _uof.AlunoRepository.GetAsync(id);
        if(aluno is null)
        {
            return NotFound($"Aluno de id = {id} não encontrado!");
        }

        var alunoDto = _mapper.Map<AlunoDTO>(aluno);

        return Ok(alunoDto);
    }

    [HttpGet("Turma/{id:int}")]
    public async Task<ActionResult<IEnumerable<AlunoDTO>>> GetAlunosTurma(int id)
    {
        var alunos = await _uof.AlunoRepository.GetAlunosTurmaAsync(id);
        if( alunos is null)
        {
            return NotFound($"Alunos não encontrados na turma de id = {id}!");
        }

        var alunosDto = _mapper.Map<IEnumerable<AlunoDTO>>(alunos);

        return Ok(alunosDto);
    }

    [HttpPost]
    public async Task<ActionResult<AlunoDTO>> Post(AlunoDTO alunoDto)
    {
        if(alunoDto is null)
        {
            return BadRequest("Aluno não recebido na requisição!");
        }

        var aluno = _mapper.Map<Aluno>(alunoDto);
        _uof.AlunoRepository.Create(aluno);
        await _uof.CommitAsync();
        alunoDto = _mapper.Map<AlunoDTO>(aluno);
        _logger.LogInformation($"Aluno de id = {alunoDto.AlunoId} criado!");

        return Ok(alunoDto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<AlunoDTO>> Put(int id, AlunoDTO alunoDto)
    {
        if(alunoDto.AlunoId != id)
        {
            return BadRequest($"Aluno com divergência de id na requisição!");
        }

        var aluno = _mapper.Map<Aluno>(alunoDto);
        _uof.AlunoRepository.Update(aluno);
        await _uof.CommitAsync();
        _logger.LogInformation($"Aluno de id = {id} atualizado!");

        return Ok(alunoDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<AlunoDTO>> Delete(int id)
    {
        var aluno = await _uof.AlunoRepository.GetAsync(id);
        if(aluno is null)
        {
            return NotFound($"Aluno de id = {id} não encontrado!");
        }

        _uof.AlunoRepository.Delete(aluno);
        await _uof.CommitAsync();
        var alunoDto = _mapper.Map<AlunoDTO>(aluno);
        _logger.LogInformation($"Aluno de id = {id} excluído!");

        return Ok(alunoDto);
    }
}
