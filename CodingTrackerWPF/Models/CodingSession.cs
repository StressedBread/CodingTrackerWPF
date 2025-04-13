namespace CodingTrackerWPF.Models;

public class CodingSession
{
    public Int32 Id { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public TimeSpan Duration { get; set; }

    public string StartDateTimeFormatted => StartDateTime.ToString("dd.MM.yyyy HH:mm:ss");
    public string EndDateTimeFormatted => EndDateTime.ToString("dd.MM.yyyy HH:mm:ss");

    public CodingSession(DateTime startDateTime, DateTime endDateTime, TimeSpan duration)
    {
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        Duration = duration;
    }

    public CodingSession(Int32 id, DateTime startDateTime, DateTime endDateTime)
    {
        Id = id;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
    }

    public CodingSession(Int32 id, DateTime startDateTime, DateTime endDateTime, TimeSpan duration)
    {
        Id = id;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        Duration = duration;
    }
}
