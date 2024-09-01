using HandlingExcelWithWpf.ViewModels;
using MahApps.Metro.Controls;

namespace HandlingExcelWithWpf.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : MetroWindow
{
    public MainWindow()
    {
        InitializeComponent();
        this.DataContext = new MainWindowViewModel();
    }
}
