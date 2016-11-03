using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuModel
{
  public class Square : ICellCollection
  {
    public int Id { get; set; }
    public List<Cell> Cells { get; set; }
    public Square (int id)
    {
      Id = id;
      Cells = new List<Cell> (Matrix.Size);
    }
  }
}
