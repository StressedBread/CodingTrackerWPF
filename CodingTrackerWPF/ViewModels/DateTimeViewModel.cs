using CodingTrackerWPF.Interfaces;
using CodingTrackerWPF.Models;
using CodingTrackerWPF.State;
using CodingTrackerWPF.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CodingTrackerWPF.ViewModels;

internal partial class DateTimeViewModel : ObservableObject
{
    [ObservableProperty]
    public ObservableCollection<CodingSession> codingSessions = new();

    public ICommand? AddCommand { get; }

    private readonly ICodingSessionService _codingSessionService;
    private readonly IDateTimeDialogService _dateTimeDialogService;
    private readonly ICodingSessionBuilder _codingSessionBuilder;

    public DateTimeViewModel(ICodingSessionService codingSessionService, IDateTimeDialogService dateTimeDialogService, ICodingSessionBuilder codingSessionBuilder)
    {
        _codingSessionService = codingSessionService;
        _dateTimeDialogService = dateTimeDialogService;
        _codingSessionBuilder = codingSessionBuilder;

        AddCommand = new AsyncRelayCommand(AddSessionAsync);
    }

    public async Task LoadSessionsAsync()
    {
        var sessions = await _codingSessionService.ViewSessionsAsync();
        if (sessions != null)
        {
            CodingSessions = new ObservableCollection<CodingSession>(sessions);
        }
    }

    private async Task AddSessionAsync()
    {
        var startDateTime = await _dateTimeDialogService.GetSessionDateTimeStartAsync();
        if (startDateTime == null) return;

        var endDateTime = await _dateTimeDialogService.GetSessionDateTimeEndAsync();
        if (endDateTime == null) return;

        var session = await _codingSessionBuilder.CreateValidatedSessionAsync(startDateTime, endDateTime);
        if (session == null) return;

        _codingSessionService.AddSession(session);
        await LoadSessionsAsync();
    }

    private async Task UpdateSessionAsync(string id, string? otherColumnValue, string? header)
    {
        Int32 sessionId = Int32.Parse(id);
        DateTime otherDateTime = DateTime.Parse(otherColumnValue ?? string.Empty);
        CodingSession? session;

        switch (header)
        {
            case "Start Date Time":
                var startDateTime = await _dateTimeDialogService.GetSessionDateTimeStartAsync();
                session = await _codingSessionBuilder.CreateValidatedSessionAsync(startDateTime, otherDateTime);
                
                if (session == null) return;

                session.Id = sessionId;
                _codingSessionService.UpdateStartTime(session);
                break;

            case "End Date Time":
                var endDateTime = await _dateTimeDialogService.GetSessionDateTimeStartAsync();
                session = await _codingSessionBuilder.CreateValidatedSessionAsync(otherDateTime, endDateTime);

                if (session == null) return;

                session.Id = sessionId;
                _codingSessionService.UpdateEndTime(session);
                break;

            default:
                break;
        }

        await LoadSessionsAsync();
    }

    public async Task HandleSelectedCell(DataGridCellInfo selectedCell, DataGrid dataGrid)
    {
        var item = selectedCell.Item;
        var column = selectedCell.Column;
        var columnHeader = column.Header;        

        object? otherDateTimeItem = null;
        DataGridColumn? otherDateTimeColumn = null;
        string? otherColumnValue = null;

        var idColumn = dataGrid.Columns[0];
        string? idCellValue = null;

        if (column == null) return;
        
        otherDateTimeColumn = (string)columnHeader == "Start Date Time" ? dataGrid.Columns[2] : dataGrid.Columns[1];
        otherDateTimeItem = dataGrid.Items[dataGrid.Items.IndexOf(item)];

        if (otherDateTimeColumn?.GetCellContent(otherDateTimeItem) is TextBlock columnTextBlock)
        {
            otherColumnValue = columnTextBlock.Text;
        }     

        if (idColumn?.GetCellContent(item) is TextBlock idTextBlock)
        {
            idCellValue = idTextBlock.Text;

            await UpdateSessionAsync(idCellValue, otherColumnValue, columnHeader.ToString());
        }
    }
}