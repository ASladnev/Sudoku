using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuModel;
using System.Collections.Generic;

namespace UnitTestSudoku
{
  [TestClass]
  public class UnitTestCalcGroup
  {
    [TestMethod]
    public void TestMethodSmallGroup ()
    {
      var m = Matrix.CreateSudokuMatrix ();

      var g = m.Horizontals [0];

      g.Cells[6].Possibilities.AddRange (new List<int> { 1, 2, 3 });
      g.Cells[8].Possibilities.AddRange (new List<int> { 1, 2, 3 });

      for (var i = 0; i < Matrix.Size; i++) {
        var cell = g.Cells[i];
        if (cell.Possibilities.Count == 0)
          cell.SetValue (i + 1, true);
      }

      var changed = m.CalcCellCollection (g);

      Assert.IsFalse (changed);
      CollectionAssert.AreEqual (new List<int> { 1, 2, 3 }, g.Cells[6].Possibilities);
      CollectionAssert.AreEqual (new List<int> { 1, 2, 3 }, g.Cells[8].Possibilities);
    }


    
    [TestMethod]
    public void TestMethodBigGroup ()
    {
      var m = Matrix.CreateSudokuMatrix ();
      var g = m.Verticals[3];
      g.Cells[0].Possibilities.AddRange (new[] { 1, 2, 3 });
      g.Cells[1].Possibilities.AddRange (new[] { 5, 6 });
      g.Cells[2].SetValue (4, true);
      g.Cells[3].Possibilities.AddRange (new[] {1, 2, 3  });
      g.Cells[4].Possibilities.AddRange (new[] {1, 5, 6, 7 });
      g.Cells[5].Possibilities.AddRange (new[] {5, 7, 9 });
      g.Cells[6].Possibilities.AddRange (new[] {1, 2, 3});
      g.Cells[7].Possibilities.AddRange (new[] {2, 3, 5, 9 });
      g.Cells[8].Possibilities.AddRange (new[] {5, 6 });

      var changed = m.CalcCellCollection (g);

      Assert.IsTrue (changed);
      CollectionAssert.AreEqual (new[] { 1, 2, 3 }, g.Cells[0].Possibilities);
      CollectionAssert.AreEqual (new[] { 5, 6 }, g.Cells[1].Possibilities);
      Assert.AreEqual (4, g.Cells[2].Value);
      CollectionAssert.AreEqual (new[] { 1, 2, 3 }, g.Cells[3].Possibilities);
      Assert.AreEqual (7, g.Cells[4].Value);
      CollectionAssert.AreEqual (new[] { 7, 9 }, g.Cells[5].Possibilities);
      CollectionAssert.AreEqual (new[] { 1, 2, 3 }, g.Cells[6].Possibilities);
      Assert.AreEqual (9, g.Cells[7].Value);
      CollectionAssert.AreEqual (new[] { 5, 6 }, g.Cells[8].Possibilities);
    }


    /*
        [TestMethod]
        public void TestMethod1 ()
        {
        }
    */
  }
}
