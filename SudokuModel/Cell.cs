using System.Collections.Generic;

namespace SudokuModel
{
  public class Cell
  {
    public int Id { get; set; }
    public List<int> Possibilities { get; set; }
    public Horizontal Horizontal { get; set; }
    public Vertical Vertical { get; set; }
    public Square Square { get; set; }
    public int? Value { get; private set; }
    public bool Initial { get; private set; }
    public Matrix Matrix { get; set; }

    public Cell (int id)
    {
      Id = id;
      Possibilities = new List<int> (Matrix.Size);
    }

    public void SetValue (int value, bool initial = false)
    {
      if (Value.HasValue) throw new SetValueAlreadySetException ();
      if (value > Matrix.Size || value < 1)
        throw new SetValueOutRangeException ();
      Value = value;
      Initial = initial;
    }

    public void CalculateCell ()
    {
      if (Horizontal == null || Vertical == null || Square == null) throw new CellNotCompleteException ();
      if (Value.HasValue) return;
      var countPossibilities = Possibilities.Count;
      PreparePossibilities ();
      CalculateCell (Horizontal);
      CalculateCell (Vertical);
      CalculateCell (Square);
      if (countPossibilities != 0 && Possibilities.Count != countPossibilities) {
        if (Matrix.Changed == false) Matrix.Changed = true;
      }
      if (Possibilities.Count == 1) {
        Value = Possibilities[0];
        Possibilities.Clear ();
        if (Matrix.Changed == false) Matrix.Changed = true;
      }
    }

    private void PreparePossibilities ()
    {
      for (var i = 1; i <= Matrix.Size; i++)
        if (Possibilities.IndexOf (i) < 0) Possibilities.Add (i);
      Possibilities.Sort ();
    }

    public void CalculateCell (ICellCollection cellCollection)
    {
      cellCollection.Cells.ForEach (c => {
      if (c == this) return;
      if (c.Value != null)
        Possibilities.Remove (c.Value.Value);
      });
    }

    public int X   
    {
      get {
        return (Id % Matrix.Size == 0) ? Matrix.Size : Id % Matrix.Size;
      }
    }

    public int Y
    {
      get {
        var y = Id / (Matrix.Size) + 1;
        if (y == Matrix.Size + 1) y = Matrix.Size;
        return y;
      }
    }

  }
}
