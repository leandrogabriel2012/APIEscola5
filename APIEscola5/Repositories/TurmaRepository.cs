using APIEscola5.Context;
using APIEscola5.Models;
using APIEscola5.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIEscola5.Repositories;

public class TurmaRepository : Repository<Turma>, ITurmaRepository
{
    public TurmaRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Turma>> GetTurmasSalaAsync(int id)
    {
        return await _context.Turmas.Where(t => t.SalaId == id).ToListAsync();
    }
    public async Task<string?> GetNomeTurmaAsync(int id)
    {
        var turma = await _context.Turmas.FindAsync(id);

        return turma?.Nome;
    }
}
