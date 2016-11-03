using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuModel;


namespace UnitTestSudoku
{
  [TestClass]
  public class UnitTestCell
  {
    [TestMethod]
    public void TestMethodSetValue ()
    {
      var cell = new Cell (7);
      cell.SetValue (9);
      Assert.AreEqual (9, cell.Value);
      Assert.IsFalse (cell.Initial);

      cell = new Cell (8);
      cell.SetValue (5, true);
      Assert.AreEqual (5, cell.Value);
      Assert.IsTrue (cell.Initial);
    }

    [TestMethod]
    [ExpectedException (typeof (SetValueOutRangeException))]
    public void TestMethodSetValueValid1 ()
    {
      var cell = new Cell (7);
      cell.SetValue (Matrix.Size + 1);
    }

    [TestMethod]
    [ExpectedException (typeof (SetValueOutRangeException))]
    public void TestMethodSetValueValid2 ()
    {
      var cell = new Cell (7);
      cell.SetValue (0);
    }

    [TestMethod]
    [ExpectedException (typeof (SetValueAlreadySetException))]
    public void TestMethodSetValueAlreadySet ()
    {
      var cell = new Cell (7);
      cell.SetValue (4);
      cell.SetValue (5);
    }



  }
}
