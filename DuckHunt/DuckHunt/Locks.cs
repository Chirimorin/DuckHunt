using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt
{
    public static class Locks
    {
        // Nieuw
        public static object InputContainer = new object { };
        public static object UnitContainer = new object { };

        // Oud
        public static object ActionContainer = new object { };
        public static object GameState = new object { };
        public static object DrawBehaviors = new object { };
        public static object DrawHelperContainer = new object { };
        
        public static object MoveBehaviors = new object { };
        
    }
}
