using QH.Application.DTOs;
using QH.Application.Interfaces;
using QH.Domain.Repositories;

namespace QH.Application.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly IWorkCalendarService _calendar;

    public TaskService(ITaskRepository taskRepository, IWorkCalendarService calendar)
    {
        _taskRepository = taskRepository;
        _calendar = calendar;
    }

    public async Task<IEnumerable<TaskDto>> GetByHouseIdAsync(int houseId)
    {
        var tasks = await _taskRepository.GetByHouseIdAsync(houseId);

        return tasks.Select(t =>
        {
            var endDate = _calendar.CalculateEndDate(t.StartDate, t.DurationDays);

            DateOnly? baselineEndDate = t.BaselineStartDate.HasValue && t.BaselineDurationDays.HasValue
                ? _calendar.CalculateEndDate(t.BaselineStartDate.Value, t.BaselineDurationDays.Value)
                : null;

            int? daysDelayed = baselineEndDate.HasValue
                ? _calendar.CountWorkingDays(baselineEndDate.Value, endDate)
                : null;

            return new TaskDto
            {
                Id = t.Id,
                HouseId = t.HouseId,
                Name = t.Name,
                CraftsmanId = t.CraftsmanId,
                CraftsmanName = t.Craftsman?.Name,
                StartDate = t.StartDate,
                DurationDays = t.DurationDays,
                EndDate = endDate,
                BaselineStartDate = t.BaselineStartDate,
                BaselineDurationDays = t.BaselineDurationDays,
                BaselineEndDate = baselineEndDate,
                DaysDelayed = daysDelayed,
                IsCompleted = t.IsCompleted
            };
        });
    }
}
