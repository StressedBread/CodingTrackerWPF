using CodingTrackerWPF.Interfaces;
using CodingTrackerWPF.Models;
using CodingTrackerWPF.State;
using CodingTrackerWPF.Views;
using MaterialDesignThemes.Wpf;
using System.Windows.Controls;

namespace CodingTrackerWPF.Services;

public class DateTimeDialogService : IDateTimeDialogService
{
    public async Task<DateTime?> GetSessionDateTimeStartAsync()
    {
        DateTimeDialogState.Instance.SessionType = "Select start of the session";

        var startResult = await OpenDialogAsync();
        if (startResult is not DateTimeModel startModel) return null;

        var startDateTime = startModel.SelectedDate.Date + startModel.SelectedTime.TimeOfDay;

        return startDateTime;
    }

    public async Task<DateTime?> GetSessionDateTimeEndAsync()
    {
        DateTimeDialogState.Instance.SessionType = "Select end of the session";

        var endResult = await OpenDialogAsync();
        if (endResult is not DateTimeModel endModel) return null;

        var endDateTime = endModel.SelectedDate.Date + endModel.SelectedTime.TimeOfDay;

        return endDateTime;
    }

    public async Task<object?> OpenDialogAsync()
    {
        var dialog = new DateTimePickerView();

        var wrapper = new ContentControl { Content = dialog };

        return await DialogHost.Show(wrapper, "RootDialog");
    }
}
