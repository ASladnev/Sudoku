using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using SudokuModel;
using System.IO;

namespace SudokuWpf
{
  public partial class MainWindow : Window
  {
    public Matrix Matrix { get; set; }
    public List<ButtonCell> ButtonList { get; set; }

    private void PrepareSudokuWindow ()
    {
      if (Matrix == null) Matrix = Matrix.CreateSudokuMatrix ();

      ButtonList = new List<ButtonCell> ();
      var grid = MatrixGrid;

      for (var i = 0; i < Matrix.Size; i++)
        grid.ColumnDefinitions.Add (new ColumnDefinition ());

      for (var i = 0; i <= Matrix.Size + 1; i++)
        grid.RowDefinitions.Add (new RowDefinition ());

      var kc = 0;
      for (var x = 0; x < Matrix.Size; x++) 
        for (var y = 0; y < Matrix.Size; y++) {
          var button = new ButtonCell ();
          button.SetValue (Grid.ColumnProperty, y);
          button.SetValue (Grid.RowProperty, x);
          button.Margin = new Thickness (2);
          button.Cell = Matrix.Cells[kc++];
          grid.Children.Add (button);
          ButtonList.Add (button);
          button.Click += ClickButton;
        }

      Action<Button, string, int> setButton = (b, s, c) => {
        b.SetValue (Grid.RowProperty, Matrix.Size + 1);
        b.SetValue (Grid.ColumnProperty, c);
        b.SetValue (Grid.ColumnSpanProperty, 3);
        b.Content = s;
        b.Width = 100;
        grid.Children.Add (b);
      };

      var buttonOpen = new Button ();
      setButton (buttonOpen, "Открыть файлы", 0);
      buttonOpen.Click += ButtonOpen_Click;

      var buttonSave = new Button ();
      setButton (buttonSave, "Сохранить файлы", 3);
      buttonSave.Click += ButtonSave_Click;

      var buttonCalc = new Button ();
      setButton (buttonCalc, "Расчитать файлы", 6);
      buttonCalc.Click += ButtonCalc_Click;
    }

    private void ButtonCalc_Click (object sender, RoutedEventArgs e)
    {
      throw new NotImplementedException ();
    }

    private void ButtonOpen_Click (object sender, RoutedEventArgs e)
    {
      var dlg = new Microsoft.Win32.OpenFileDialog ();
      dlg.DefaultExt = ".sdk";
      dlg.Filter = "Sudoku documents (.sdk)|*.sdk";
      dlg.InitialDirectory = @"D:\";
      bool? result = dlg.ShowDialog ();
      if (result != true) return;
      var filename = dlg.FileName;
      using (var sr = new StreamReader (filename)) {
        while (!sr.EndOfStream) {
          var rl = sr.ReadLine ();
        }
        
      }
    }

    private void ButtonSave_Click (object sender, RoutedEventArgs e)
    {
      using (var sw = new StreamWriter (@"D:\1.sdk")) {
        Matrix.Cells.ForEach (c => sw.WriteLine ($"{c.Id} {c.Value}"));
      }
    }

    private void ClickButton (object sender, RoutedEventArgs e)
    {
      var button = sender as ButtonCell;
      string result = Convert.ToString (button.Cell.Value ?? -1);
      if (result == "-1") result = "";
      var inputNumber = new InputNumber ();
      inputNumber.ButtonCell = button;
      if (inputNumber.ShowDialog () != true) return;
      var number = Convert.ToInt32 (inputNumber.Answer);
      if (number < 1 || number > 9) return;
      button.Cell.SetValue (number, true);
      button.Content = number;
    }
  }
}
