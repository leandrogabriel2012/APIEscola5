using APIEscola5.Models;

namespace APIEscola5.Repositories.Interfaces;

public interface IAlunoRepository : IRepository<Aluno>
{
    Task<IEnumerable<Aluno>> GetAlunosTurmaAsync(int id);
}
