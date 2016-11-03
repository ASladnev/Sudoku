using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuModel
{
  public class Horizontal : ICellCollection
  {
    public int Id { get; set; }
    public List<Cell> Cells { get; set; }

    public Horizontal (int id)
    {
      Id = id;
      Cells = new List<Cell> (Matrix.Size);
    }
  }
}
