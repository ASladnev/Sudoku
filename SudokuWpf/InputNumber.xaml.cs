using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SudokuWpf
{
  /// <summary>
  /// Логика взаимодействия для InputNumber.xaml
  /// </summary>
  public partial class InputNumber : Window
  {
    public InputNumber ()
    {
      InitializeComponent ();
      Loaded += (o, p) => {
        Application curApp = Application.Current;
        Window mainWindow = curApp.MainWindow;
        Left = mainWindow.Left + (mainWindow.Width - ActualWidth) / 2;
        Top = mainWindow.Top + (mainWindow.Height - ActualHeight) / 2;
        txtAnswer.Focus ();
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
