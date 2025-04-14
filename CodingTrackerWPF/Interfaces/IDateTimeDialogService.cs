namespace CodingTrackerWPF.Interfaces;

public interface IDateTimeDialogService
{
    Task<DateTime?> GetSessionDateTimeStartAsync(DateTime? start, DateTime? end);
    Task<DateTime?> GetSessionDateTimeEndAsync(DateTime? start, DateTime? end);
    Task<object?> OpenDialogAsync(DateTime? start, DateTime? end);
}
