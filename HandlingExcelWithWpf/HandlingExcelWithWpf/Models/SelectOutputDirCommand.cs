using HandlingExcelWithWpf.ViewModels;
using System.Windows.Input;

namespace HandlingExcelWithWpf.Models;

public class SelectOutputDirCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;

    private MainWindowViewModel _MainWindowViewModel;

    public SelectOutputDirCommand(MainWindowViewModel mainWindowViewModel)
    {
        _MainWindowViewModel = mainWindowViewModel;
    }
    
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        // Configure open folder dialog box
        Microsoft.Win32.OpenFolderDialog dialog = new();

        dialog.Multiselect = false;
        dialog.Title = "Select an output folder";

        // Show open folder dialog box
        bool? result = dialog.ShowDialog();

        // Process open folder dialog box results
        if (result == true)
        {
            // Get the selected folder
            _MainWindowViewModel.SelectedOutputDirName = dialog.FolderName;
        }
    }
}
