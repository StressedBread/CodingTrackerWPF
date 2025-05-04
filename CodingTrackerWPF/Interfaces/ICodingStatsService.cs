using CodingTrackerWPF.Models;

namespace CodingTrackerWPF.Interfaces;

public interface ICodingStatsService
{
    TimeSpan GetTotalTimeCoded(List<CodingSession> codingSessions);
    int GetNumberOfTotalSessions(List<CodingSession> codingSessions);
    TimeSpan GetAverageSessionDuration(List<CodingSession> codingSessions);
    TimeSpan GetLongestSessionDuration(List<CodingSession> codingSessions);
    TimeSpan GetShortestSessionDuration(List<CodingSession> codingSessions);
    TimeSpan GetTotalTimeCodedToday(List<CodingSession> codingSessions);
    TimeSpan GetTotalTimeCodedThisWeek(List<CodingSession> codingSessions);
    TimeSpan GetTotalTimeCodedThisMonth(List<CodingSession> codingSessions);
    TimeSpan GetAverageDailyTimeThisWeek(List<CodingSession> codingSessions);
    List<TimeSpan> GetTimeCodedPerDayThisWeek(List<CodingSession> codingSessions);
}
