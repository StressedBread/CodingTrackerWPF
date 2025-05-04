using CodingTrackerWPF.Interfaces;
using CodingTrackerWPF.Models;
using System.Collections.ObjectModel;
using System.Globalization;

namespace CodingTrackerWPF.Services;

public class FilteringService : IFilteringService
{
    public List<CodingSession> Filtering(ObservableCollection<CodingSession> sessions, FiltersModel filters)
    {
        var filteredSessions = sessions.AsQueryable();

        if (!string.IsNullOrEmpty(filters.Period))
        {
            var start = GetDateRangeFromPeriod(filters.Period);

            if (start != null)
            {
                filteredSessions = filteredSessions.Where(s => s.StartDateTime >= start);
            }
        }

        if (filters.StartDate != null && filters.EndDate != null)
        {
            filteredSessions = filteredSessions.Where(s => s.StartDateTime >= filters.StartDate && s.EndDateTime <= filters.EndDate);
        }

        if (filters.MinDuration != null && filters.MaxDuration != null)
        {
            filteredSessions = filteredSessions.Where(s => s.Duration >= filters.MinDuration && s.Duration <= filters.MaxDuration);
        }

        if (filters.DayOfWeek != null)
        {
            filteredSessions = filteredSessions.Where(s => s.StartDateTime.DayOfWeek == filters.DayOfWeek);
        }

        return filteredSessions.ToList();
    }

    public DateTime? GetDateRangeFromPeriod(string period)
    {
        var now = DateTime.Now;

        if (period.StartsWith("Week"))
        {
            if (int.TryParse(period.Replace("Week", "").Trim(), out int weekNumber))
            {
                var yearStart = new DateTime(now.Year, 1, 1);
                var startOfWeek = yearStart.AddDays((weekNumber - 1) * 7);

                int dayDiff = (int)startOfWeek.DayOfWeek - (int)DayOfWeek.Monday;
                if (dayDiff < 0) dayDiff += 7;
                startOfWeek = startOfWeek.AddDays(-dayDiff);

                return startOfWeek;
            }
        }
        else if (DateTime.TryParseExact(period, "MMMM", CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out DateTime monthDate))
        {
            var year = now.Year;
            var month = monthDate.Month;

            var startOfMonth = new DateTime(year, month, 1);
            return startOfMonth;
        }
        else if (int.TryParse(period, out int year))
        {
            var startOfYear = new DateTime(year, 1, 1);
            return startOfYear;
        }

        return null;
    }
}
