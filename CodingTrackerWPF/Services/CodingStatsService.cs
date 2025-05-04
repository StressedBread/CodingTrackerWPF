using CodingTrackerWPF.Interfaces;
using CodingTrackerWPF.Models;

namespace CodingTrackerWPF.Services;

public class CodingStatsService : ICodingStatsService
{
    public TimeSpan GetTotalTimeCoded(List<CodingSession> codingSessions)
    {
        return codingSessions.Aggregate(TimeSpan.Zero, (sum, session) => sum + session.Duration);

    }

    public int GetNumberOfTotalSessions(List<CodingSession> codingSessions)
    {
        return codingSessions.Count;
    }

    public TimeSpan GetAverageSessionDuration(List<CodingSession> codingSessions)
    {
        if (codingSessions.Count == 0)
            return TimeSpan.Zero;

        var totalDuration = GetTotalTimeCoded(codingSessions);
        var original = TimeSpan.FromTicks(totalDuration.Ticks / codingSessions.Count);
        return TimeSpan.FromSeconds(Math.Round(original.TotalSeconds));
    }

    public TimeSpan GetLongestSessionDuration(List<CodingSession> codingSessions)
    {
        return codingSessions.Max(session => session.Duration);
    }

    public TimeSpan GetShortestSessionDuration(List<CodingSession> codingSessions)
    {
        return codingSessions.Min(session => session.Duration);
    }

    public TimeSpan GetTotalTimeCodedToday(List<CodingSession> codingSessions)
    {
        var today = DateTime.Today;

        return codingSessions
            .Where(session => session.StartDateTime.Date == today)
            .Aggregate(TimeSpan.Zero, (sum, session) => sum + session.Duration);
    }

    public TimeSpan GetTotalTimeCodedThisWeek(List<CodingSession> codingSessions)
    {
        var firstDayOfWeek = DateTime.Today.AddDays(-(((int)DateTime.Today.DayOfWeek + 6) % 7));

        return codingSessions
            .Where(session => session.StartDateTime >= firstDayOfWeek)
            .Aggregate(TimeSpan.Zero, (sum, session) => sum + session.Duration);
    }

    public TimeSpan GetTotalTimeCodedThisMonth(List<CodingSession> codingSessions)
    {
        var firstDayOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

        return codingSessions
            .Where(session => session.StartDateTime >= firstDayOfMonth)
            .Aggregate(TimeSpan.Zero, (sum, session) => sum + session.Duration);
    }

    public TimeSpan GetAverageDailyTimeThisWeek(List<CodingSession> codingSessions)
    {
        var firstDayOfWeek = DateTime.Today.AddDays(-(((int)DateTime.Today.DayOfWeek + 6) % 7));
        var sessionsThisWeek = codingSessions
            .Where(session => session.StartDateTime >= firstDayOfWeek)
            .ToList();

        if (sessionsThisWeek.Count == 0)
            return TimeSpan.Zero;

        var totalDuration = GetTotalTimeCoded(sessionsThisWeek);
        var daysInWeek = (int)(DateTime.Today - firstDayOfWeek).TotalDays + 1;
        var original = TimeSpan.FromTicks(totalDuration.Ticks / daysInWeek);
        return TimeSpan.FromSeconds(Math.Round(original.TotalSeconds));
    }

    public List<TimeSpan> GetTimeCodedPerDayThisWeek(List<CodingSession> codingSessions)
    {
        var firstDayOfWeek = DateTime.Today.AddDays(-(((int)DateTime.Today.DayOfWeek + 6) % 7));
        var sessionsThisWeek = codingSessions
            .Where(session => session.StartDateTime >= firstDayOfWeek)
            .ToList();
        var timeCodedPerDay = new List<TimeSpan>(7);
        for (int i = 0; i < 7; i++)
        {
            var day = firstDayOfWeek.AddDays(i);
            var dailyDuration = sessionsThisWeek
                .Where(session => session.StartDateTime.Date == day.Date)
                .Aggregate(TimeSpan.Zero, (sum, session) => sum + session.Duration);
            timeCodedPerDay.Add(dailyDuration);
        }
        return timeCodedPerDay;
    }
}
