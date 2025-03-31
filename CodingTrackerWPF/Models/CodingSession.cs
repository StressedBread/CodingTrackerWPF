namespace CodingTrackerWPF.Models;

internal class CodingSession
{
    public long Id { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public TimeSpan Duration { get; set; }

    public string StartDateTimeFormatted => StartDateTime.ToString("dd.MM.yyyy HH:mm:ss");
    public string EndDateTimeFormatted => EndDateTime.ToString("dd.MM.yyyy HH:mm:ss");

    public CodingSession(long id, DateTime startDateTime, DateTime endDateTime, TimeSpan duration)
    {
        Id = id;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        Duration = duration;
    }
}
