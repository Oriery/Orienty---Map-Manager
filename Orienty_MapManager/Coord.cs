using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orienty_MapManager
{
    public class Coord
    {
        public Coord(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }
    }
}
