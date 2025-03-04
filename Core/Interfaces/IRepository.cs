namespace Core.Interfaces;

public interface IRepository<T> where T : class
{
    public Task AddAsync(T entity);
    public Task<IEnumerable<T>> GetAllAsync();
    public Task<IQueryable<T>> GetAllAsQueryableAsync();
    public Task<T> GetById(Guid id);
    public Task<List<T>> GetPaginatedAsync(int pageNumber, int pageSize);
    public Task UpdateAsync(T entity);
    public Task DeleteAsync(Guid id);
}