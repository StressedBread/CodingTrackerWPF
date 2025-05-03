using CodingTrackerWPF.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using System.Windows.Input;

namespace CodingTrackerWPF.ViewModels;

public partial class WeeklyGoalDialogViewModel : ObservableObject
{
    [ObservableProperty]
    private int weeklyGoalValue;

    public ICommand SubmitCommand => new RelayCommand(() =>
    {
        DialogHost.Close("HomeRootDialog", new WeeklyGoalModel(TimeSpan.FromHours(WeeklyGoalValue)) );
    });
}
