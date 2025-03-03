namespace Core.Interfaces;

public interface IRepository<T> where T : class
{
    public Task AddAsync(T entity);
    public Task<IEnumerable<T>> GetAll();
    public Task<IEnumerable<T>> GetPaginated(int pageNumber, int pageSize);
    public Task Update(T entity);
}