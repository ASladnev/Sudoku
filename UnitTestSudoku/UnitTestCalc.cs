using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuModel;
using System.Collections.Generic;

namespace UnitTestSudoku
{
  [TestClass]
  public class UnitTestCalc
  {
    [TestMethod]
    public void TestMethodCalcCellOnHorizontal ()
    {
      var cell = new Cell (4);
      cell.Possibilities.AddRange (new[] {1, 2 , 3, 4, 5, 6, 7, 8, 9});
      cell.Horizontal = new Horizontal (1);

      var cells = cell.Horizontal.Cells;
      for (var i = 0; i < Matrix.Size; i++) {
        if (i == 3) cells.Add (cell);
        else cells.Add (new Cell (i + 1));
      }

      cells[0].SetValue (1);
      cells[2].SetValue (3, true);
      cells[4].SetValue (5);
      cells[6].SetValue (7, true);
      cells[8].SetValue (9);

      cell.CalculateCell (cell.Horizontal);
      CollectionAssert.AreEqual (new List<int> { 2, 4, 6, 8 }, cell.Possibilities);
      Assert.IsNull (cell.Value);
    }

    [TestMethod]
    public void TestMethotCalcOnVertical ()
    {
      var cell = new Cell (4);
      cell.Possibilities.AddRange (new[] { 2, 4, 6, 8 });
      cell.Vertical = new Vertical (4);

      var cells = cell.Vertical.Cells;
      for (var i = 0; i < Matrix.Size; i++) {
        if (i == 3) cells.Add (cell);
        else cells.Add (new Cell (i + 1));
      }

      cells[0].SetValue (1);
      cells[2].SetValue (3, true);
      cells[4].SetValue (8);
      cells[6].SetValue (4, true);
      cells[8].SetValue (9);

      cell.CalculateCell (cell.Vertical);
      CollectionAssert.AreEqual (new List<int> { 2, 6 }, cell.Possibilities);
      Assert.IsNull (cell.Value);
    }

    [TestMethod]
    public void TestMethotCalcOnSquar ()
    {
      var cell = new Cell (4);
      cell.Possibilities.AddRange (new[] { 2, 6});
      cell.Square = new Square (7);

      var cells = cell.Square.Cells;
      for (var i = 0; i < Matrix.Size; i++) {
        if (i == 3) cells.Add (cell);
        else cells.Add (new Cell (i + 1));
      }

      cells[0].SetValue (1);
      cells[2].SetValue (2, true);
      cells[4].SetValue (8);
      cells[6].SetValue (4, true);
      cells[8].SetValue (3);

      cell.CalculateCell (cell.Square);
      CollectionAssert.AreEqual (new List<int> { 6 }, cell.Possibilities);
    }

    [TestMethod]
    [ExpectedException (typeof (CellNotCompleteException))]
    public void TestMethodCellNotComplete ()
    {
      var cell = new Cell (4);
      cell.CalculateCell ();
    }
  }
}
