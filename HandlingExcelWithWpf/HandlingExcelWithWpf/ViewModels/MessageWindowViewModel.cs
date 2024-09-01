namespace HandlingExcelWithWpf.ViewModels;

public class MessageWindowViewModel
{
    public string WindowTitle => "Message Box";
    
    public string Message { get; }
    
    public MessageWindowViewModel(string message)
    {
        this.Message = message;
    }
}
