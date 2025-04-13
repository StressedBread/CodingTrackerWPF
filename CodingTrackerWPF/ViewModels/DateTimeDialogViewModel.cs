using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using System.Windows.Input;
using CodingTrackerWPF.Models;
using CodingTrackerWPF.State;

namespace CodingTrackerWPF.ViewModels;

public partial class DateTimeDialogViewModel : ObservableObject
{
    [ObservableProperty]
    private DateTime selectedDate = DateTime.Now;
    [ObservableProperty]
    private DateTime selectedTime = DateTime.Now;

    public string Session => DateTimeDialogState.Instance.SessionType;

    public ICommand SubmitCommand => new RelayCommand(() =>
    {
        DialogHost.Close("RootDialog", new DateTimeModel
        {
            SelectedDate = SelectedDate,
            SelectedTime = SelectedTime
        });
    });
}
