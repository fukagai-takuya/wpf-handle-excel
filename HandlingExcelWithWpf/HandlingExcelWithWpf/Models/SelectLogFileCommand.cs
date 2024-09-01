using HandlingExcelWithWpf.ViewModels;
using System.Windows.Input;

namespace HandlingExcelWithWpf.Models;

public class SelectLogFileCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;

    private MainWindowViewModel _MainWindowViewModel;

    public SelectLogFileCommand(MainWindowViewModel mainWindowViewModel)
    {
        _MainWindowViewModel = mainWindowViewModel;
    }
    
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        var dialog = new Microsoft.Win32.OpenFileDialog();
        dialog.DefaultExt = ".txt"; // Default file extension
        dialog.Filter = "State Inquiry Log File (.txt)|*.txt"; // Filter files by extension

        // Show open file dialog box
        bool? result = dialog.ShowDialog();

        // Process open file dialog box results
        if (result == true)
        {
            _MainWindowViewModel.SelectedLogFileName = dialog.FileName;            
        }        
    }
}
