using APIEscola5.Context;
using APIEscola5.Models;
using APIEscola5.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIEscola5.Repositories;

public class AlunoRepository : Repository<Aluno>, IAlunoRepository
{
    public AlunoRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Aluno>> GetAlunosTurmaAsync(int id)
    {
        return await _context.Alunos.Where(a => a.TurmaId == id).ToListAsync();
    }
}
