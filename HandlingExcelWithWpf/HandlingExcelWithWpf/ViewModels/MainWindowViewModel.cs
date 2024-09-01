using CommunityToolkit.Mvvm.Input;
using HandlingExcelWithWpf.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace HandlingExcelWithWpf.ViewModels;

public class MainWindowViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public string WindowTitle => "Handle Excel files with NPOI";

    public string SelectFormatFileButtonName => "Select a format Excel file";

    private string m_SelectedFormatFileName = "";
    public string SelectedFormatFileName
    {
        get => m_SelectedFormatFileName;
        set
        {
            m_SelectedFormatFileName = value;
            NotifyPropertyChanged(nameof(SelectedFormatFileName));
            this.GenerateExcelFile.NotifyCanExecuteChanged();
        }
    }

    public string SelectLogFileButtonName => "Select a log file";

    private string m_SelectedLogFileName = "";
    public string SelectedLogFileName
    {
        get => m_SelectedLogFileName;
        set
        {
            m_SelectedLogFileName = value;
            NotifyPropertyChanged(nameof(SelectedLogFileName));
            this.GenerateExcelFile.NotifyCanExecuteChanged();
        }
    }

    public string SelectOutputDirButtonName => "Select an output folder";
    
    private string m_SelectedOutputDirName = "";
    public string SelectedOutputDirName
    {
        get => m_SelectedOutputDirName;
        set
        {
            m_SelectedOutputDirName = value;
            NotifyPropertyChanged(nameof(SelectedOutputDirName));
            this.GenerateExcelFile.NotifyCanExecuteChanged();
        }
    }
    
    public ICommand SelectFormatFile { get; }

    public ICommand SelectLogFile { get; }

    public ICommand SelectOutputDir { get; }    

    public string GenerateExcelFileButtonName => "Generate an Excel file";

    public IRelayCommand GenerateExcelFile { get; }    
    
    public MainWindowViewModel()
    {
        this.SelectFormatFile = new SelectFormatFileCommand(this);        
        this.SelectLogFile = new SelectLogFileCommand(this);
        this.SelectOutputDir = new SelectOutputDirCommand(this);        
        this.GenerateExcelFile = new GenerateExcelFileCommand(this);
    }
}
