using System.Diagnostics;

namespace Core.Interfaces;

public interface IActivityRepository : IRepository<Activity>
{
    public Task<IEnumerable<Activity>> GetByName(string name);
}