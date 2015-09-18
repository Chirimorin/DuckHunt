using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt
{
    public static class Locks
    {
        public static object ActionContainer = new object { };
        public static object GameState = new object { };
        public static object DrawBehaviors = new object { };
        public static object MoveBehaviors = new object { };
        public static object UnitContainer = new object { };
    }
}
