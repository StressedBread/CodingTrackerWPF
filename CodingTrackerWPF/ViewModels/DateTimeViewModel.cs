using CodingTrackerWPF.Interfaces;
using CodingTrackerWPF.Models;
using CodingTrackerWPF.State;
using CodingTrackerWPF.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CodingTrackerWPF.ViewModels;

internal partial class DateTimeViewModel : ObservableObject
{
    [ObservableProperty]
    public ObservableCollection<CodingSession> codingSessions = new();

    public ICommand? OpenCommand { get; }

    private DateTime? _combinedStartDateTime = null;
    private DateTime? _combinedEndDateTime = null;

    private readonly ICodingSessionService _codingSessionService;

    public DateTimeViewModel(ICodingSessionService codingSessionService)
    {
        _codingSessionService = codingSessionService;

        OpenCommand = new AsyncRelayCommand(OpenDialog);
    }

    public async Task LoadSessionsAsync()
    {
        var sessions = await _codingSessionService.ViewSessionsAsync();
        if (sessions != null)
        {
            CodingSessions = new ObservableCollection<CodingSession>(sessions);
        }
    }

    public async Task<object?> OpenDialogAsync()
    {        
        var dialog = new DateTimePickerView();

        var wrapper = new ContentControl
        {
            Content = dialog
        };

        return await DialogHost.Show(wrapper, "RootDialog");
    }

    private async Task OpenDialog()
    {
        DateTimeDialogState.Instance.SessionType = "Select start of the session";

        var startResult = await OpenDialogAsync();
        if (startResult is not DateTimeModel startModel) return;

        _combinedStartDateTime = startModel.SelectedDate.Date + startModel.SelectedTime.TimeOfDay;

        DateTimeDialogState.Instance.SessionType = "Select end of the session";

        await Task.Delay(500);

        var endResult = await OpenDialogAsync();
        if (endResult is not DateTimeModel endModel) return;

        _combinedEndDateTime = endModel.SelectedDate.Date + endModel.SelectedTime.TimeOfDay;

        await SendCombinedDateTime(_combinedStartDateTime, _combinedEndDateTime);

        await LoadSessionsAsync();
    }

    private async Task SendCombinedDateTime(DateTime? startDateTime, DateTime? endDateTime)
    {
        if (startDateTime == null || endDateTime == null)
        {
            return;
        }

        DateTime startDateTimeActual = startDateTime.Value;
        DateTime endDateTimeActual = endDateTime.Value;
        TimeSpan duration = endDateTimeActual - startDateTimeActual;

        if (duration < TimeSpan.Zero)
        {
            var messageDialogView = new MessageDialogView();
            messageDialogView.DataContext = new MessageDialogViewModel("Error", "End cannot be earlier than start!");
            await DialogHost.Show(messageDialogView, "RootDialog");
            return;
        }

        CodingSession codingSession = new(startDateTimeActual, endDateTimeActual, duration);

        _codingSessionService.AddSession(codingSession);
    }
}