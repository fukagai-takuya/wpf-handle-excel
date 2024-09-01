using HandlingExcelWithWpf.ViewModels;
using MahApps.Metro.Controls;
using System.Windows;

namespace HandlingExcelWithWpf.Views;

/// <summary>
/// Interaction logic for MessageWindow.xaml
/// </summary>
public partial class MessageWindow : MetroWindow
{
    public MessageWindow(string message)
    {
        InitializeComponent();
        this.DataContext = new MessageWindowViewModel(message);
    }

    public static void Show(string message)
    {
        var messageWindow = new MessageWindow(message);
        messageWindow.ShowDialog();
    }

    private void OkIsClicked(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}
