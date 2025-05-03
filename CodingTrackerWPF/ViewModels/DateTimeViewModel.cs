using CodingTrackerWPF.Interfaces;
using CodingTrackerWPF.Models;
using CodingTrackerWPF.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MySqlX.XDevAPI;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace CodingTrackerWPF.ViewModels;

internal partial class DateTimeViewModel : ObservableObject
{
    private FilteringService _filteringService = new();

    [ObservableProperty]
    public ObservableCollection<CodingSession> codingSessions = [];
    [ObservableProperty]
    public DataGrid? mainDataGrid;
    [ObservableProperty]
    public DataGridRow? selectedRow;

    public IAsyncRelayCommand? AddCommand { get; }
    public IAsyncRelayCommand? DeleteCommand { get; }
    public IAsyncRelayCommand? FiltersCommand { get; }
    public IAsyncRelayCommand? ResetFiltersCommand { get; }

    private readonly ICodingSessionService _codingSessionService;
    private readonly IDateTimeDialogService _dateTimeDialogService;
    private readonly ICodingSessionBuilder _codingSessionBuilder;
    private readonly IFiltersDialogService _filtersDialogService;

    private readonly string _rootDialogID = "RootDialog";

    public DateTimeViewModel(ICodingSessionService codingSessionService, IDateTimeDialogService dateTimeDialogService, 
        ICodingSessionBuilder codingSessionBuilder, IFiltersDialogService filtersDialogService)
    {
        _codingSessionService = codingSessionService;
        _dateTimeDialogService = dateTimeDialogService;
        _codingSessionBuilder = codingSessionBuilder;
        _filtersDialogService = filtersDialogService;

        AddCommand = new AsyncRelayCommand(AddSessionAsync);
        DeleteCommand = new AsyncRelayCommand(DeleteSessionAsync, CanDeleteSession);
        FiltersCommand = new AsyncRelayCommand(FiltersDialogAsync);
        ResetFiltersCommand = new AsyncRelayCommand(LoadSessionsAsync);
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
        var startDateTime = await _dateTimeDialogService.GetSessionDateTimeStartAsync(_rootDialogID, null, null);
        if (startDateTime == null) return;

        await Task.Delay(500);

        var endDateTime = await _dateTimeDialogService.GetSessionDateTimeEndAsync(_rootDialogID, null, null);
        if (endDateTime == null) return;

        var session = await _codingSessionBuilder.CreateValidatedSessionAsync(startDateTime, endDateTime);
        if (session == null) return;

        _codingSessionService.AddSession(session);
        await LoadSessionsAsync();
    }

    private async Task UpdateSessionAsync(string id, string? currentColumnValue, string? otherColumnValue, string? header)
    {
        Int32 sessionId = Int32.Parse(id);
        DateTime otherDateTime = DateTime.Parse(otherColumnValue ?? string.Empty);
        DateTime currentDateTime = DateTime.Parse(currentColumnValue ?? string.Empty);
        CodingSession? session;

        switch (header)
        {
            case "Start Date Time":
                var startDateTime = await _dateTimeDialogService.GetSessionDateTimeStartAsync(_rootDialogID, currentDateTime, null);
                session = await _codingSessionBuilder.CreateValidatedSessionAsync(startDateTime, otherDateTime);
                
                if (session == null) return;

                session.Id = sessionId;
                _codingSessionService.UpdateStartTime(session);
                break;

            case "End Date Time":
                var endDateTime = await _dateTimeDialogService.GetSessionDateTimeEndAsync(_rootDialogID, null, currentDateTime);
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
    private async Task DeleteSessionAsync()
    {
        if (MainDataGrid == null || SelectedRow == null) return;

        var item = SelectedRow.Item;
        int columnIndex = 0;

        if (columnIndex < MainDataGrid.Columns.Count)
        {
            var column = MainDataGrid.Columns[columnIndex];
            var cellContent = column.GetCellContent(item);
            if (cellContent is TextBlock textBlock)
            {
                if (Int32.TryParse(textBlock.Text, out Int32 cellValue))
                {
                    _codingSessionService.DeleteSession(cellValue);
                    await LoadSessionsAsync();
                }
            }
        }
    }

    private async Task FiltersDialogAsync()
    {
        var filters = await _filtersDialogService.GetFiltersAsync();
        if (filters == null) return;

        var filteredSessions = _filteringService.Filtering(CodingSessions, filters);
        if (filteredSessions != null)
        {
            CodingSessions = new ObservableCollection<CodingSession>(filteredSessions);
        }
    }

    public async Task HandleSelectedCell(DataGridCellInfo selectedCell, DataGrid dataGrid)
    {
        var item = selectedCell.Item;
        var column = selectedCell.Column;
        var columnHeader = column.Header;       
        string? currentColumnValue = null;

        object? otherDateTimeItem;
        DataGridColumn? otherDateTimeColumn;
        string? otherColumnValue = null;

        var idColumn = dataGrid.Columns[0];
        string? idCellValue;

        if (column == null) return;
        
        otherDateTimeColumn = (string)columnHeader == "Start Date Time" ? dataGrid.Columns[2] : dataGrid.Columns[1];
        otherDateTimeItem = dataGrid.Items[dataGrid.Items.IndexOf(item)];

        if (otherDateTimeColumn?.GetCellContent(otherDateTimeItem) is TextBlock columnTextBlock)
        {
            otherColumnValue = columnTextBlock.Text;
        }     

        if (column.GetCellContent(item) is TextBlock currentTextBlock)
        {
            currentColumnValue = currentTextBlock.Text;
        }

        if (idColumn?.GetCellContent(item) is TextBlock idTextBlock)
        {
            idCellValue = idTextBlock.Text;

            await UpdateSessionAsync(idCellValue, currentColumnValue, otherColumnValue, columnHeader.ToString());
        }
    }

    partial void OnSelectedRowChanged(DataGridRow? oldValue, DataGridRow? newValue)
    {
        DeleteCommand?.NotifyCanExecuteChanged();
    }

    partial void OnMainDataGridChanged(DataGrid? oldValue, DataGrid? newValue)
    {
        DeleteCommand?.NotifyCanExecuteChanged();
    }

    private bool CanDeleteSession() => SelectedRow != null && MainDataGrid != null;
}