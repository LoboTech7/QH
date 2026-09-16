using QH.Application.Interfaces;

namespace QH.Application.Services;

public class WorkCalendarService : IWorkCalendarService
{
    public DateOnly CalculateEndDate(DateOnly startDate, int durationDays)
    {
        var current = startDate;
        var workDaysRemaining = durationDays;

        while (workDaysRemaining > 0)
        {
            if (IsWorkingDay(current))
                workDaysRemaining--;

            if (workDaysRemaining > 0)
                current = current.AddDays(1);
        }

        return current;
    }

    public int CountWorkingDays(DateOnly startDate, DateOnly endDate)
    {
        var count = 0;
        var current = startDate;

        while (current <= endDate)
        {
            if (IsWorkingDay(current))
                count++;
            current = current.AddDays(1);
        }

        return count;
    }

    public bool IsWorkingDay(DateOnly date)
    {
        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            return false;

        return !IsDanishHoliday(date);
    }

    private bool IsDanishHoliday(DateOnly date)
    {
        var holidays = GetDanishHolidays(date.Year);
        return holidays.Contains(date);
    }

    private HashSet<DateOnly> GetDanishHolidays(int year)
    {
        var easterSunday = CalculateEasterSunday(year);

        return new HashSet<DateOnly>
        {
            // Faste helligdage
            new DateOnly(year, 1, 1),   // Nytårsdag
            new DateOnly(year, 12, 24), // Juleaften
            new DateOnly(year, 12, 25), // 1. juledag
            new DateOnly(year, 12, 26), // 2. juledag
            new DateOnly(year, 12, 31), // Nytårsaften

            // Bevægelige helligdage (beregnet fra påske)
            easterSunday.AddDays(-3),   // Skærtorsdag
            easterSunday.AddDays(-2),   // Langfredag
            easterSunday,               // Påskedag
            easterSunday.AddDays(1),    // 2. påskedag
            easterSunday.AddDays(26),   // Store bededag (4. fredag efter påske)
            easterSunday.AddDays(39),   // Kristi himmelfartsdag
            easterSunday.AddDays(49),   // Pinsedag
            easterSunday.AddDays(50),   // 2. pinsedag
        };
    }

    // Anonym gregoriansk algoritme til beregning af påskedag
    private DateOnly CalculateEasterSunday(int year)
    {
        int a = year % 19;
        int b = year / 100;
        int c = year % 100;
        int d = b / 4;
        int e = b % 4;
        int f = (b + 8) / 25;
        int g = (b - f + 1) / 3;
        int h = (19 * a + b - d - g + 15) % 30;
        int i = c / 4;
        int k = c % 4;
        int l = (32 + 2 * e + 2 * i - h - k) % 7;
        int m = (a + 11 * h + 22 * l) / 451;
        int month = (h + l - 7 * m + 114) / 31;
        int day = ((h + l - 7 * m + 114) % 31) + 1;

        return new DateOnly(year, month, day);
    }
}
