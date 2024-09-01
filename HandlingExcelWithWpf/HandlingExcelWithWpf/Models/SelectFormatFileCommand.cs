using HandlingExcelWithWpf.ViewModels;
using System.Windows.Input;

namespace HandlingExcelWithWpf.Models;

public class SelectFormatFileCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;

    private MainWindowViewModel _MainWindowViewModel;

    public SelectFormatFileCommand(MainWindowViewModel mainWindowViewModel)
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
        dialog.Filter = "Excel Format File (.xlsx)|*.xlsx"; // Filter files by extension

        // Show open file dialog box
        bool? result = dialog.ShowDialog();

        // Process open file dialog box results
        if (result == true)
        {
            _MainWindowViewModel.SelectedFormatFileName = dialog.FileName;
        }        
    }
}
