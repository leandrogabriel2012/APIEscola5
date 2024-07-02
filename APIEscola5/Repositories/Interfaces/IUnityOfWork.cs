namespace APIEscola5.Repositories.Interfaces;

public interface IUnityOfWork
{
    IAlunoRepository AlunoRepository { get; }
    ITurmaRepository TurmaRepository { get; }
    ISalaRepository SalaRepository { get; }
    Task CommitAsync();
}
