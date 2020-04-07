using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_1_to_N
{
    public class Cell
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public int Value { get; set; }

        public Cell( int row, int col, int value )
        {
            Row = row;
            Col = col;
            Value = value;
        }
    }
}
