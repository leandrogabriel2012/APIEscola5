namespace APIEscola5.Repositories.Interfaces;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetAsync(int id);
    T Create(T entity);
    T Update(T entity);
    T Delete(T entity);
}
