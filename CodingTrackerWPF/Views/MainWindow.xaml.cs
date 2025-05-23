﻿using CodingTrackerWPF.Interfaces;
using CodingTrackerWPF.Services;
using CodingTrackerWPF.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace CodingTrackerWPF.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly MainWindowViewModel _viewModel;

    public MainWindow()
    {
        InitializeComponent();

        var queryService = new QueryService();

        ICodingSessionService codingSessionService = new CodingSessionService(queryService);
        IWeeklyGoalService weeklyGoalService = new WeeklyGoalService(queryService);
        _viewModel = new MainWindowViewModel(codingSessionService, weeklyGoalService);
        DataContext = _viewModel;

        MainContent.Content = new HomeView();
    }

    private void OnDrawerItemClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            switch (button.Tag)
            {
                case "HomeButton":
                    MainContent.Content = new HomeView();
                    break;
                case "CodingSessionsButton":
                    MainContent.Content = new CodingSessionsView();
                    break;
                case "LiveCodingSessionButton":
                    MainContent.Content = new LiveCodingSessionView();
                    break;
                case "ExitButton":
                    Application.Current.Shutdown();
                    break;
                default:
                    break;
            }
        }
    }
}