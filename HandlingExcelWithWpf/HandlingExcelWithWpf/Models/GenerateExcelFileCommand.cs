using CommunityToolkit.Mvvm.Input;
using HandlingExcelWithWpf.ViewModels;
using HandlingExcelWithWpf.Views;
using NPOI.SS.UserModel;
using System.IO;
using System.Text.RegularExpressions;

namespace HandlingExcelWithWpf.Models;

public class GenerateExcelFileCommand : IRelayCommand
{
    public event EventHandler? CanExecuteChanged;

    public void NotifyCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }    

    private MainWindowViewModel _MainWindowViewModel;

    private const string _SheetName = "SampleSheet";

    public GenerateExcelFileCommand(MainWindowViewModel mainWindowViewModel)
    {
        _MainWindowViewModel = mainWindowViewModel;
    }

    public bool CanExecute(object? parameter)
    {
        if (_MainWindowViewModel.SelectedFormatFileName != "" &&
            _MainWindowViewModel.SelectedLogFileName != "" &&
            _MainWindowViewModel.SelectedOutputDirName != "")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Execute(object? parameter)
    {
        OutputExcelFile();
    }

    private void OutputExcelFile()
    {
        // ブック読み込み
        IWorkbook book = WorkbookFactory.Create(_MainWindowViewModel.SelectedFormatFileName);

        // シート名からシート取得
        ISheet sheet = book.GetSheet(_SheetName);

        int sheetNum = 1;

        StreamReader sr = new StreamReader(_MainWindowViewModel.SelectedLogFileName);

        var logLines = new List<string>();
        
        while (true)
        {
            // Read the next line
            var line = sr.ReadLine();

            if (line == null || line.Trim().Length == 0)
            {
                if (sheetNum > 1)
                {
                    // Prepare a new Excel sheet by copying a current sheet
                    sheet = sheet.CopySheet($"{_SheetName}({sheetNum})");
                }
                    
                WriteLogToSheet(sheet, logLines);
                sheetNum++;

                if (line == null)
                {
                    break;
                }
            }
            else
            {
                logLines.Add(line);
            }
        }

        // close the file
        sr.Close();

        DateTime dateTime = DateTime.Now;
        string dateTimeStr = dateTime.ToString("yyyy-MM-dd-HH-mm-ss");
        string outputFileName = $"{_MainWindowViewModel.SelectedOutputDirName}\\StateSheets{dateTimeStr}.xlsx";
        
        using(var fs = new FileStream(outputFileName, FileMode.Create))
        {
            book.Write(fs);
        }

        MessageWindow.Show("Finished");
    }

    private void WriteCell(ISheet sheet, int rowIndex, int columnIndex, string outputString)
    {
        var row = sheet.GetRow(rowIndex) ?? sheet.CreateRow(rowIndex);
        var cell = row.GetCell(columnIndex) ?? row.CreateCell(columnIndex);
        cell.CellStyle.WrapText = true;
        cell.SetCellValue(outputString);
    }

    private Regex _RegDate = new Regex(@"(\d{4}/\d{2}/\d{2})");
    private Regex _RegItem = new Regex(@"([-a-z]+):(.+)");

    private void WriteLogToSheet(ISheet sheet, List<string> logLines)
    {
        foreach (var line in logLines)
        {
            var match = _RegDate.Match(line);
            if (match.Success)
            {
                var dateStr = match.Groups[1].Value.Trim();
                WriteCell(sheet, 2, 1, dateStr);
                continue;
            }

            match = _RegItem.Match(line);                
            if (match.Success)
            {
                var keyStr = match.Groups[1].Value.Trim();
                var valueStr = match.Groups[2].Value.Trim();

                if (keyStr == "push-up")
                {
                    WriteCell(sheet, 5, 1, valueStr);
                }
                else if (keyStr == "sit-up")
                {
                    WriteCell(sheet, 8, 1, valueStr);                    
                }
                else if (keyStr == "chin-up")
                {
                    WriteCell(sheet, 11, 1, valueStr);                    
                }
                else if (keyStr == "note")
                {
                    WriteCell(sheet, 14, 1, valueStr);
                }                
            }
        }
        
        logLines.Clear();
    }
}
