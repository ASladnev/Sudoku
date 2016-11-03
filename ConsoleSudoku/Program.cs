using SudokuModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSudoku
{
  class Program
  {
    static void Main (string[] args)
    {
      var m = Matrix.CreateSudokuMatrix ();

#region Initial date
      m.SetCells (new Dictionary<int, int> {
        {11 , 7}, {12 , 6}, {18 , 5},

        {19, 3}, {21 , 8}, {24 , 9}, {26 , 4}, {27 , 2},

        {29 , 8}, {31 , 7}, {35 , 1},

        {37 , 5}, {42 , 3},

        {48 , 9}, {49 , 2}, {51 , 4}, {52 , 5},

        {64 , 2}, {65 , 6}, {70 , 8},

        {75 , 4}, {78 , 5}, {80 , 6}, {81 , 3},

      });
#endregion

      // Master loop -----------------------------------------------
      do {
        do {
          m.CalculateOneTime ();
        }
        while (m.Changed);
        m.CalculationOneTimeGroup ();
      } while (m.Changed);
      //------------------------------------------------------------

      // Output ----------------------------------------------------
      m.Horizontals.ForEach (h => {
        for (int i = 0; i < Matrix.Size; i++)
          Console.Write ($"{h.Cells[i].Value}_|");
        Console.WriteLine ("");
      });

      m.Cells.ForEach (c => {
        if (c.Possibilities.Count == 0) return;
        Console.Write ($"{c.Id}:   ");
        c.Possibilities.ForEach (p => Console.Write ($"{p}  "));
        Console.WriteLine ();
      });
      // -----------------------------------------------------------
    }
  }
}
