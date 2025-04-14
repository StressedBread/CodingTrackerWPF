namespace CodingTrackerWPF.Interfaces;

public interface IDateTimeDialogService
{
    Task<DateTime?> GetSessionDateTimeStartAsync();
    Task<DateTime?> GetSessionDateTimeEndAsync();
    Task<object?> OpenDialogAsync();
}
