using CommunityToolkit.Mvvm.ComponentModel;

namespace CodingTrackerWPF.State;

public partial class DateTimeDialogState : ObservableObject
{
    private static DateTimeDialogState? _instance;
    public static DateTimeDialogState Instance
    {
        get
        {
            _instance ??= new DateTimeDialogState();
            return _instance;
        }
    }

    [ObservableProperty]
    public string sessionType = "Start";
    public bool Opened { get; set; } = false;
}
