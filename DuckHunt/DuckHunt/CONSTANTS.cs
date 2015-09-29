using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt
{
    public static class CONSTANTS
    {
        public static readonly int FPS_LIMIT = 60; // -1 voor geen limiet
        public static readonly double fixedTimeCalls = 0.02; // Tijd tussen elke fixed time call. 

        // Grootte voor de canvas. Wordt automatisch geschaald naar scherm grootte. 
        public static readonly int CANVAS_WIDTH = 1280; 
        public static readonly int CANVAS_HEIGHT = 720; 
    }
}
