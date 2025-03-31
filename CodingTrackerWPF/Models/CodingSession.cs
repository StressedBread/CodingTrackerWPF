namespace CodingTrackerWPF.Models;

internal class CodingSession
{
    public long Id { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public TimeSpan Duration { get; set; }

    public CodingSession(long id, DateTime startDateTime, DateTime endDateTime, TimeSpan duration)
    {
        Id = id;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        Duration = duration;
    }
}
