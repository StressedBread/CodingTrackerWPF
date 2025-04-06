using CommunityToolkit.Mvvm.ComponentModel;

namespace CodingTrackerWPF.ViewModels;

internal class DialogViewModel : ObservableObject
{
    [ObservableProperty]
    private DateTime? _date;
}
