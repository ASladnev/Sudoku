using System;
using System.Windows;

namespace SudokuWpf
{
  /// <summary>
  /// Логика взаимодействия для InputNumber.xaml
  /// </summary>
  public partial class InputNumber : Window
  {
    public ButtonCell ButtonCell { get; set; }

    public InputNumber ()
    {
      InitializeComponent ();
      Loaded += (o, p) => {
        var curApp = Application.Current;
        var mainWindow = curApp.MainWindow;
        Left = mainWindow.Left + (mainWindow.Width - ActualWidth) / 2;
        Top = mainWindow.Top + (mainWindow.Height - ActualHeight) / 2;
        txtAnswer.Focus ();
        lblQuestion.Content = string.Format ($"Описание ячейки № {ButtonCell.Cell.Id}  X= {ButtonCell.Cell.X}  Y= {ButtonCell.Cell.Y}");
      };
    }

    public string Answer
    {
      get { return txtAnswer.Text; }
    }

    private void btnDialogOk_Click (object sender, RoutedEventArgs e)
    {
      DialogResult = true;
    }

  }
}
