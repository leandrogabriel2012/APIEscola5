using APIEscola5.Models;

namespace APIEscola5.Repositories.Interfaces;

public interface ITurmaRepository : IRepository<Turma>
{
    Task<IEnumerable<Turma>> GetTurmasSalaAsync(int id);

    Task<string?> GetNomeTurmaAsync(int id);
}
