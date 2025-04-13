namespace CodingTrackerWPF.ViewModels;

public class MessageDialogViewModel
{
    public string Title { get; set; }
    public string Message { get; set; }
    public string OkButtonText { get; set; }
    public MessageDialogViewModel(string title, string message, string okButtonText = "OK")
    {
        Title = title;
        Message = message;
        OkButtonText = okButtonText;
    }
}
