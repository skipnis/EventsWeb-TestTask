namespace Core.Interfaces;

public interface IRepository<T> where T : class
{
    public Task AddAsync(T entity, CancellationToken cancellationToken);
    public Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
    public Task<T> GetById(Guid id, CancellationToken cancellationToken);
    public Task<List<T>> GetPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    public void UpdateAsync(T entity);
    public void DeleteAsync(T entity);
}