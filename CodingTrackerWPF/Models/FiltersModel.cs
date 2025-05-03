namespace CodingTrackerWPF.Models;

public class FiltersModel(string period,
                    DayOfWeek? dayOfWeek = null,
                    DateTime? startDate = null,
                    DateTime? endDate = null,
                    TimeSpan? minDuration = null,
                    TimeSpan? maxDuration = null)
{
    public string Period { get; set; } = period;
    public DayOfWeek? DayOfWeek { get; set; } = dayOfWeek;
    public DateTime? StartDate { get; set; } = startDate;
    public DateTime? EndDate { get; set; } = endDate;    
    public TimeSpan? MinDuration { get; set; } = minDuration;
    public TimeSpan? MaxDuration { get; set; } = maxDuration;
}
