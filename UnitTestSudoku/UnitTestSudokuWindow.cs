using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuWpf;

namespace UnitTestSudoku
{
  [TestClass]
  public class UnitTestSudokuWindow
  {
    [TestMethod]
    public void TestMethodCountBottom ()
    {
      var mw = new MainWindow ();
      Assert.AreEqual (81, mw.ButtonList.Count);
    }
  }
}
