using CodingTrackerWPF.Interfaces;
using CodingTrackerWPF.Models;
using CodingTrackerWPF.Services;
using CodingTrackerWPF.State;
using CodingTrackerWPF.ViewModels;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CodingTrackerWPF.Views;

/// <summary>
/// Interaction logic for CodingSessionsView.xaml
/// </summary>
public partial class CodingSessionsView : UserControl
{
    private bool _isUpdating = false;

    public CodingSessionsView()
    {
        InitializeComponent();

        ICodingSessionService codingSessionService = new CodingSessionService();
        IDateTimeDialogService dateTimeDialogService = new DateTimeDialogService();
        ICodingSessionBuilder codingSessionBuilder = new CodingSessionBuilder();

        DataContext = new DateTimeViewModel(codingSessionService, dateTimeDialogService, codingSessionBuilder);

        MainDataGrid.MouseDoubleClick += MainDataGrid_MouseDoubleClick;
        MainDataGrid.PreviewMouseLeftButtonDown += MainDataGrid_PreviewMouseLeftButtonDown;
    }

    private void MainDataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (DataContext is DateTimeViewModel viewModel && sender is DataGrid dataGrid)
        {
            var point = e.GetPosition(dataGrid);
            var hit = dataGrid.InputHitTest(point) as DependencyObject;

            while (hit != null && hit is not DataGridRow)
            {
                hit = VisualTreeHelper.GetParent(hit);
            }

            if (hit is DataGridRow row)
            {
                viewModel.SelectedRow = row;
                viewModel.MainDataGrid = dataGrid;
            }
            else
            {
                viewModel.SelectedRow = null;
                viewModel.MainDataGrid = null;
            }
        }        
    }

    private async void MainDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (_isUpdating) return;

        if (DataContext is DateTimeViewModel viewModel && sender is DataGrid dataGrid && dataGrid.CurrentCell.Column is DataGridTextColumn column)
        {
            _isUpdating = true;

            var selectedCell = dataGrid.CurrentCell;
            await viewModel.HandleSelectedCell(selectedCell, dataGrid);

            _isUpdating = false;
        }
    }

    private async void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        if (DataContext is DateTimeViewModel viewModel)
        {
            await viewModel.LoadSessionsAsync();
        }
    }
}
