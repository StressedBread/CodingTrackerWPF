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
    private DateTime selectedDate;
    [ObservableProperty]
    private DateTime selectedTime;

    private readonly string _dialogID;

    public DateTimeDialogViewModel(string dialogID, DateTime? initalStartDate = null, DateTime? initialEndDate = null)
    {
        if (initalStartDate != null)
        {
            SelectedDate = initalStartDate.Value;
            SelectedTime = initalStartDate.Value;
        }
        else if (initialEndDate != null)
        {
            SelectedDate = initialEndDate.Value;
            SelectedTime = initialEndDate.Value;
        }
        else
        {
            SelectedDate = DateTime.Now;
            SelectedTime = DateTime.Now;
        }

        _dialogID = dialogID;
    }

    public static string Session => DateTimeDialogState.Instance.SessionType;

    public ICommand SubmitCommand => new RelayCommand(() =>
    {
        DialogHost.Close(_dialogID, new DateTimeModel
        {
            SelectedDate = SelectedDate,
            SelectedTime = SelectedTime
        });
    });
}
