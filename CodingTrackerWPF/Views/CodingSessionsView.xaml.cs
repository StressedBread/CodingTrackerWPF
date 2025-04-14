using CodingTrackerWPF.Interfaces;
using CodingTrackerWPF.Models;
using CodingTrackerWPF.Services;
using CodingTrackerWPF.State;
using CodingTrackerWPF.ViewModels;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Controls;

namespace CodingTrackerWPF.Views;

/// <summary>
/// Interaction logic for CodingSessionsView.xaml
/// </summary>
public partial class CodingSessionsView : UserControl
{
    private ObservableCollection<CodingSession>? CodingSessions { get; set; }
    private bool _isUpdating = false;

    public CodingSessionsView()
    {
        InitializeComponent();

        ICodingSessionService codingSessionService = new CodingSessionService();
        IDateTimeDialogService dateTimeDialogService = new DateTimeDialogService();
        ICodingSessionBuilder codingSessionBuilder = new CodingSessionBuilder();

        DataContext = new DateTimeViewModel(codingSessionService, dateTimeDialogService, codingSessionBuilder);

        MainDataGrid.SelectedCellsChanged += MainDataGrid_SelectedCellsChanged;

        //Mockup();
    }

    private async void MainDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
    {
        if (_isUpdating) return;

        if (DataContext is DateTimeViewModel viewModel && sender is DataGrid dataGrid)
        {
            _isUpdating = true;

            var selectedCell = dataGrid.CurrentCell;
            await viewModel.HandleSelectedCell(selectedCell, dataGrid);

            _isUpdating = false;
        }
    }

    private void Mockup()
    {
        CodingSessions = new ObservableCollection<CodingSession>
        {
            new CodingSession(1, DateTime.Now, DateTime.Now.AddHours(1)),
            new CodingSession(2, DateTime.Now.AddHours(2), DateTime.Now.AddHours(3)),
            new CodingSession(3, DateTime.Now.AddHours(4), DateTime.Now.AddHours(5)),
        };

        MainDataGrid.ItemsSource = CodingSessions;
    }

    private async void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        if (DataContext is DateTimeViewModel viewModel)
        {
            await viewModel.LoadSessionsAsync();
        }
    }
}
