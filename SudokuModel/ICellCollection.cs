using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuModel
{
  public interface ICellCollection
  {
    int Id { get; set; }
    List<Cell> Cells { get; set; }
  }
}
