using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using SudokuModel;

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

      var buttonSave = new Button ();
      setButton (buttonSave, "Сохранить файлы", 3);

      var buttonCalc = new Button ();
      setButton (buttonCalc, "Расчитать файлы", 6);
    }

    private void ClickButton (object sender, RoutedEventArgs e)
    {
      var button = sender as ButtonCell;
      string result = Convert.ToString (button.Cell.Value ?? -1);
      if (result == "-1") result = "";
      var inputNumber = new InputNumber ();
      inputNumber.ButtonCell = button;
      //inputNumber.SetParent = this;
      if (inputNumber.ShowDialog () != true) return;
      var number = Convert.ToInt32 (inputNumber.Answer);
      if (number < 1 || number > 9) return;
      button.Cell.SetValue (number, true);
      button.Content = number;
    }
  }
}
