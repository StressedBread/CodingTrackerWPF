using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;

namespace CodingTrackerWPF.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    public IRelayCommand? ShowTextBox { get; }

    [ObservableProperty]
    private bool isTextBoxVisible;

    public HomeViewModel()
    {
        ShowTextBox = new RelayCommand(SetWeeklyGoal);
    }

    public Visibility TextBoxVisibility => IsTextBoxVisible ? Visibility.Visible : Visibility.Collapsed;

    public void SetWeeklyGoal()
    {
        IsTextBoxVisible = true;

        OnPropertyChanged(nameof(TextBoxVisibility));
    }
}
