using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuModel;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;

namespace UnitTestSudoku
{
  [TestClass]
  public class UnitTestModel
  {
    [TestInitialize]
    public void TestInitMatrix()
    {

    }

    [TestMethod]
    public void TestModelMatrixCreation ()
    {
      var matrix = Matrix.CreateSudokuMatrix ();
      Assert.IsFalse (matrix.Changed);

      // Cells -------------------------------------------------------
      Assert.AreEqual (Matrix.Size * Matrix.Size, matrix.Cells.Count);
      var iC = 1;
      matrix.Cells.ForEach (c => Assert.AreEqual (iC++, c.Id));

      // Horizontal --------------------------------------------------
      Assert.AreEqual (Matrix.Size, matrix.Horizontals.Count);
      int hId = 1;
      matrix.Horizontals.ForEach (h => {
        Assert.AreEqual (hId, h.Id);
        Assert.AreEqual (Matrix.Size, h.Cells.Count);
        var i = 1;
        h.Cells.ForEach (c => {
          Assert.AreEqual ((hId - 1) * Matrix.Size + i, c.Id);
          i++;
        });
        hId++;
      });

      // Vertical ----------------------------------------------------
      Assert.AreEqual (Matrix.Size, matrix.Verticals.Count);
      var vI = 1;
      matrix.Verticals.ForEach (v => {
        var vK = 1;
        v.Cells.ForEach (c => {
          var id = matrix.Horizontals[vK - 1].Cells[vI - 1].Id;
          Assert.AreEqual (c.Id, id);
          vK++;
        });
        vI++;  
      });

      // Square ------------------------------------------------------
      var fcList = new List<int> {1, 4, 7, 28, 31, 34, 55, 58, 61 };
      Assert.AreEqual (Matrix.Size, matrix.Squares.Count);
      int sI = 1;
      matrix.Squares.ForEach (s => {
        Assert.AreEqual (sI++, s.Id);
        Assert.AreEqual (fcList[s.Id - 1], s.Cells[0].Id);
        Assert.AreEqual (Matrix.Size, s.Cells.Count);
      });

      // Cells -------------------------------------------------------
      matrix.Cells.ForEach (c => {
        Assert.IsNotNull (c.Horizontal);
        Assert.IsNotNull (c.Vertical);
        Assert.IsNotNull (c.Square);
      });
      Assert.AreEqual (Matrix.Size * Matrix.Size, matrix.Cells.Count);
    }

    [TestMethod]
    public void TestModelMatrixBlankCalculate ()
    {
      var matrix = Matrix.CreateSudokuMatrix ();

      matrix.CalculateOneTime ();
      var possibilities = new List<int> ();
      possibilities.AddRange (new[] {1, 2, 3, 4, 5, 6, 7, 8, 9 });

      matrix.Cells.ForEach (c => CollectionAssert.AreEqual (possibilities, c.Possibilities));

      Assert.IsFalse (matrix.Changed);
    }

    [TestMethod]
    public void TestModelMatrixSetCell ()
    {
      var m = Matrix.CreateSudokuMatrix ();
      m.SetCell (5, 2);
      var c = m.Cells[4];
      Assert.AreEqual (2, c.Value);
      Assert.IsTrue (c.Initial);
    }

    [TestMethod]
    [ExpectedException (typeof (SetValueOutRangeException))]
    public void TestMethodMatrixSetValueRange ()
    {
      var m = Matrix.CreateSudokuMatrix ();
      m.SetCell (82, 2);
    }

    [TestMethod]
    [ExpectedException (typeof (SetValueOutRangeException))]
    public void TestMethodMatrixSetValueRange2 ()
    {
      var m = Matrix.CreateSudokuMatrix ();
      m.SetCell (2, 10);
    }
    
    [TestMethod]
    public void TestMethodSetCells ()
    {
      var m = Matrix.CreateSudokuMatrix ();
      m.SetCells (new Dictionary <int, int> {{5, 5}, {15, 7 }, {20, 9} });
      m.Cells.ForEach (c => {
          switch (c.Id) {
            case 5:
              Assert.AreEqual (5, c.Value);
              break;
            case 15:
              Assert.AreEqual (7, c.Value);
              break;
            case 20:
              Assert.AreEqual (9, c.Value);
              break;
          default:
            Assert.IsNull (c.Value);
            break;
          }
      });  
    }
  
    
    private void PrepareMatrix (Matrix matrix)
    {
      matrix.SetCells (new Dictionary<int, int> {
        { 3, 2}, { 4, 3}, {5, 7}, {9, 6},
        {12, 4}, {14, 6}, {16, 9},
        {22, 9}, {23, 1}, {26, 4}, {27, 8},
        {28, 8}, {29, 5},
        {37, 6}, {39, 3}, {40, 5}, {44, 1},
        {56, 7}, {57, 5}, {58, 1}, {59, 8}, {62, 6},
        {64, 3}, {66, 6}, {67, 7}, {72, 2},
      });
    }
      
    [TestMethod]
    public void TestModelMatrixCalculate ()
    {
      var m = Matrix.CreateSudokuMatrix ();

      PrepareMatrix (m);

      m.CalculateOneTime ();

      var result = new Dictionary<int, int[]> ();

      Action<int, int[]> r = (id, p) => result.Add (id, p);

      #region Result
      r (1, new[] {1, 5, 9});
      r (2, new[] {1, 8, 9});
      r (6, new[] {4, 5, 8});
      r (7, new[] {1, 5});

      r (10, new[] {1, 5, 7});
      r (11, new[] {1, 3, 8});
      r (13, new[] {2, 8});
      r (15, new[] {2, 5, 8});
      r (17, new[] {2, 3, 7});
      r (18, new[] {1, 3, 7});

      r (19, new[] {5, 7});
      r (20, new[] {3, 6});
      r (24, new[] {2, 5});
      r (25, new[] {2, 3});

      r (30, new[] {1, 9});
      r (31, new[] {2, 4, 6});
      r (32, new[] {2, 3, 4, 9});
      r (33, new[] {1, 2, 3, 4, 6, 7, 9});
      r (34, new[] {2, 3, 4, 6, 7});
      r (35, new[] {2, 3, 7, 9});
      r (36, new[] {3, 4, 7, 9});

      r (38, new[] {2, 4, 9});
      r (41, new[] {2, 4, 9});
      r (42, new[] {2, 4, 7, 8, 9});
      r (43, new[] {2, 4, 7, 8});
      r (45, new[] {4, 7, 9});

      r (46, new[] {1, 2, 4, 7, 9});
      r (47, new[] {1, 2, 4, 9  });
      r (48, new[] {1, 9});
      r (49, new[] {2, 4, 6, 8});
      r (50, new[] {2, 3, 4, 9});
      r (51, new[] {1, 2, 3, 4, 6, 7, 8, 9});
      r (52, new[] {2, 3, 4, 5, 6, 7, 8 });
      r (53, new[] {2, 3, 7, 8, 9});
      r (54, new[] {3, 4, 5, 7, 9});

      r (55, new[] {2, 4, 9});
      r (60, new[] {2, 3, 4, 9});
      r (61, new[] {3, 4});
      r (63, new[] {3, 4, 9});

      r (65, new[] {1, 4, 8, 9});
      r (68, new[] {4, 5, 9});
      r (69, new[] {4, 5, 9});
      r (70, new[] {1, 4, 5, 8});
      r (71, new[] {8, 9 });

      r (73, new[] {1, 2, 4, 9});
      r (74, new[] {1, 2, 4, 8, 9});
      r (75, new[] {1, 8, 9});
      r (76, new[] {2, 4, 6});
      r (77, new[] {2, 3, 4, 5, 9});
      r (78, new[] {2, 3, 4, 5, 6, 9});
      r (79, new[] {1, 3, 4, 5, 7, 8});
      r (80, new[] {3, 7, 8, 9});
      r (81, new[] {1, 3, 4, 5, 7, 9});
      #endregion

      m.Cells.ForEach (c => {
        Trace.WriteLine ($"{c.Id} {c.Possibilities.Count} {c.Value}");
        if (c.Value.HasValue) {
          Assert.AreEqual (0, c.Possibilities.Count);
          return;
        }
        
        CollectionAssert.AreEqual (result[c.Id], c.Possibilities);
      });

      Assert.AreEqual (5, m.Cells[7].Value);
      Assert.IsFalse (m.Cells[7].Initial);
      Assert.AreEqual (0, m.Cells[7].Possibilities.Count);

      Assert.AreEqual (7, m.Cells[20].Value);
      Assert.IsFalse (m.Cells[20].Initial);
      Assert.AreEqual (0, m.Cells[20].Possibilities.Count);

      Assert.IsTrue (m.Changed);
    }


    [TestMethod]
    public void TestModelMatrixCountValues ()
    {
      var m = Matrix.CreateSudokuMatrix ();
      PrepareMatrix (m);
      m.CalculateOneTime ();
      Assert.AreEqual (28, m.CountValues);
    }

    [TestMethod]
    public void TestModelMatrixCalculate2 ()
    {
      var m = Matrix.CreateSudokuMatrix ();

      PrepareMatrix (m);
      do {
        m.CalculateOneTime ();
      }
      while (m.Changed); 
      Assert.IsFalse (m.Changed);

      m.Horizontals.ForEach (h => {
        for (int i = 0; i < Matrix.Size; i++)
          Trace.Write ($"{h.Cells[i].Value}   ");
        Trace.WriteLine ("");

      });

    }


  }
}
