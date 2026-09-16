using QH.Application.DTOs;

namespace QH.Application.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<TaskDto>> GetByHouseIdAsync(int houseId);
}
