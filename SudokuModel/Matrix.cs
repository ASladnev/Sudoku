using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuModel
{
  public class Matrix
  {
    public List<Cell> Cells { get; set; }
    public List<Horizontal> Horizontals { get; set; }
    public List<Vertical> Verticals { get; set; }
    public List<Square> Squares { get; set; }
    public bool Changed { get; set; }

    private Matrix ()
    {
      Cells = new List<Cell> (Size * Size);
      Horizontals = new List<Horizontal> (Size);
      Verticals = new List<Vertical> (Size);
      Squares = new List<Square> (Size);
    }

    public const int Size = 9;
    public const int SizeSquar = Size / 3;

    public void SetCell (int cellNumber, int value)
    {
      if (cellNumber > Size * Size || cellNumber < 1 || value > Size || Size < 1)
        throw new SetValueOutRangeException ();
      var c = Cells[cellNumber - 1];
      c.SetValue (value, true);
    }

    public void SetCells (IDictionary <int, int> cellValue)
    {
      foreach (var kp in cellValue) {
        Cells[kp.Key - 1].SetValue (kp.Value);
      }
    }

    public static Matrix CreateSudokuMatrix ()
    {
      var matrix = new Matrix ();
      var horId = 1;
      Horizontal horizontal = null;

      for (var i = 0; i < Size * Size; i++) {
        var cell = new Cell (i + 1);
        cell.Matrix = matrix;
        matrix.Cells.Add (cell);

        if (i % Size == 0) {
          horizontal = new Horizontal (horId++);
          matrix.Horizontals.Add (horizontal);
        }

        horizontal.Cells.Add (cell);
        cell.Horizontal = horizontal;
      }

      for (var i = 0; i < Size; i++) {
        var vertical = new Vertical (i + 1);
        matrix.Verticals.Add (vertical);
        for (var k = 0; k < Size; k++) {
          var cell = matrix.Cells [ i + 0 + (k + 0) * Size + 0];
          vertical.Cells.Add (cell);
          cell.Vertical = vertical;
        }
      }

      for (var y = 0; y < SizeSquar; y++) {
        for (var x = 0; x < SizeSquar; x++) {
          var squar = new Square (y * SizeSquar + x + 1);
          matrix.Squares.Add (squar);
          squar.Cells = new List<Cell> (Size);
          var cellNumber = y * Size * SizeSquar + x * SizeSquar;
          squar.Cells.Add (matrix.Cells [cellNumber]);
          matrix.Cells[cellNumber].Square = squar;
          for (var cy = 0; cy < SizeSquar; cy++)
            for (var cx = 0; cx < SizeSquar; cx++) {
              if (cx == 0 && cy == 0) continue;
              var cell = matrix.Cells [cellNumber + cy * Size + cx];
              squar.Cells.Add (cell);
              cell.Square = squar;
            }
        }
      }

      matrix.Changed = false;

      return matrix;
    }

    public void CalculateOneTime ()
    {
      Changed = false;
      Cells.ForEach (c => c.CalculateCell ());
     }

    public void CalculationOneTimeGroup ()
    {
      Horizontals.ForEach (gc => CalcCellCollection (gc));
      Verticals.ForEach (gc => CalcCellCollection (gc));
      Squares.ForEach (gc => CalcCellCollection (gc));
    }

    public int CountValues
    {
      get {
        var i = 0;
        Cells.ForEach (c => { if (c.Value.HasValue) i++; });
        return i;
      }
    }

    public bool CalcCellCollection (ICellCollection cellCollection)
    {
      var changed = false;
      for (var i = 0; i < Size - 1; i++) {
        var cc = cellCollection.Cells[i];
        if (cc.Value.HasValue) continue;
        var gList = new List<Cell> {cc};
        for (var k = i + 1; k < Size; k++) {
          var ck = cellCollection.Cells[k];
          if (ck.Value.HasValue) continue;
          if (cc.Possibilities.SequenceEqual (ck.Possibilities))
            gList.Add (ck);
        }

        if (gList.Count != cc.Possibilities.Count) break;

        for (var k = i + 1; k < Size; k++) {
          var ck = cellCollection.Cells[k];
          if (ck.Value.HasValue || gList.Contains (ck)) continue;
          cc.Possibilities.ForEach (p => {
            var ch = ck.Possibilities.Remove (p);
            if (!changed && ch) changed = true;
            if (ck.Possibilities.Count == 1) {
              ck.SetValue (ck.Possibilities[0]);
              ck.Possibilities.Clear ();
            }
          });
        }
      }

      if (!Changed) Changed = changed;
      return changed;
    }

  }
}
