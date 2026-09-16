using Microsoft.EntityFrameworkCore;
using QH.Domain.Repositories;
using QH.Infrastructure.Data;

namespace QH.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async System.Threading.Tasks.Task<QH.Domain.Models.Task?> GetByIdAsync(int id)
        => await _context.Tasks.FindAsync(id);

    public async System.Threading.Tasks.Task<IEnumerable<QH.Domain.Models.Task>> GetAllAsync()
        => await _context.Tasks.ToListAsync();

    public async System.Threading.Tasks.Task<IEnumerable<QH.Domain.Models.Task>> GetByHouseIdAsync(int houseId)
        => await _context.Tasks
            .Where(t => t.HouseId == houseId)
            .Include(t => t.Craftsman)
            .OrderBy(t => t.StartDate)
            .ToListAsync();

    public async System.Threading.Tasks.Task AddAsync(QH.Domain.Models.Task entity)
        => await _context.Tasks.AddAsync(entity);

    public void Update(QH.Domain.Models.Task entity)
        => _context.Tasks.Update(entity);

    public void Delete(QH.Domain.Models.Task entity)
        => _context.Tasks.Remove(entity);

    public async System.Threading.Tasks.Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
}
