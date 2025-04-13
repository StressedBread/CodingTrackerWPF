using CodingTrackerWPF.Interfaces;
using CodingTrackerWPF.Models;
using CodingTrackerWPF.Services;
using CodingTrackerWPF.State;
using CodingTrackerWPF.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace CodingTrackerWPF.Views;

/// <summary>
/// Interaction logic for CodingSessionsView.xaml
/// </summary>
public partial class CodingSessionsView : UserControl
{
    private ObservableCollection<CodingSession>? CodingSessions { get; set; }

    public CodingSessionsView()
    {
        InitializeComponent();

        ICodingSessionService codingSessionService = new CodingSessionService();

        DataContext = new DateTimeViewModel(codingSessionService);

        //Mockup();
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
