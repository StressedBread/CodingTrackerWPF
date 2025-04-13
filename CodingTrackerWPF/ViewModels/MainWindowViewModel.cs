using CodingTrackerWPF.Interfaces;

namespace CodingTrackerWPF.ViewModels;

public class MainWindowViewModel
{
    private ICodingSessionService _codingSessionService;
    public MainWindowViewModel(ICodingSessionService codingSessionService)
    {
        _codingSessionService = codingSessionService;

        _codingSessionService.CreateCodingSessionsTable();
    }
}
