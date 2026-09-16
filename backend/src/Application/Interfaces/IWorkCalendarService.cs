namespace QH.Application.Interfaces;

public interface IWorkCalendarService
{
    DateOnly CalculateEndDate(DateOnly startDate, int durationDays);
    int CountWorkingDays(DateOnly startDate, DateOnly endDate);
    bool IsWorkingDay(DateOnly date);
}
