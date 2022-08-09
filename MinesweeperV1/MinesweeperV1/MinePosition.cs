using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperV1
{
    class MinePosition
    {
        public int row;
        public int col;


        public MinePosition(int r, int c)
        {
            row = r;
            col = c;
        }
    }
}
