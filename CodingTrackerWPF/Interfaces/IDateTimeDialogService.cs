namespace CodingTrackerWPF.Interfaces;

public interface IDateTimeDialogService
{
    Task<DateTime?> GetSessionDateTimeStartAsync(string dialogID, DateTime? start, DateTime? end);
    Task<DateTime?> GetSessionDateTimeEndAsync(string dialogID, DateTime? start, DateTime? end);
    Task<object?> OpenDialogAsync(string dialogID, DateTime? start, DateTime? end);
}
