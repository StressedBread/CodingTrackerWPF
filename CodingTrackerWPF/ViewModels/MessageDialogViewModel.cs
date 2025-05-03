namespace CodingTrackerWPF.ViewModels;

public class MessageDialogViewModel(string title, string message, string okButtonText = "OK")
{
    public string Title { get; set; } = title;
    public string Message { get; set; } = message;
    public string OkButtonText { get; set; } = okButtonText;
}
