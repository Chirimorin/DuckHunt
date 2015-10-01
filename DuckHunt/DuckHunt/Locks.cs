using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt
{
    public static class Locks
    {
        public static object ClickedPoints = new object { };
        public static object UnitContainer = new object { };
    }
}
