namespace QH.Domain.Repositories;

public interface ITaskRepository : IRepository<Models.Task>
{
    Task<IEnumerable<Models.Task>> GetByHouseIdAsync(int houseId);
}
