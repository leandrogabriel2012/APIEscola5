using APIEscola5.Context;
using APIEscola5.Repositories.Interfaces;

namespace APIEscola5.Repositories;

public class UnityOfWork : IUnityOfWork
{
    private IAlunoRepository? _alunoRepository;
    private ITurmaRepository? _turmaRepository;
    private ISalaRepository? _salaRepository;
    private AppDbContext _context;

    public UnityOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IAlunoRepository AlunoRepository
    {
        get
        { 
            return _alunoRepository = _alunoRepository ?? new AlunoRepository(_context);
        }
    }

    public ITurmaRepository TurmaRepository
    {
        get
        {
            return _turmaRepository = _turmaRepository ?? new TurmaRepository(_context);
        }
    }

    public ISalaRepository SalaRepository
    {
        get
        {
            return _salaRepository = _salaRepository ?? new SalaRepository(_context);
        }
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
