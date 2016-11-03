using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuModel
{
  public class Vertical : ICellCollection
  {
    public int Id { get; set; }
    public List<Cell> Cells { get; set; }
    public Vertical (int id)
    {
      Id = id;
      Cells = new List<Cell> (Matrix.Size);
    }
  }
}
