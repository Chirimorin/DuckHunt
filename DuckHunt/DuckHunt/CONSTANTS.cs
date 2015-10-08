using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt
{
    public static class CONSTANTS
    {
        public static readonly int UPDATES_PER_SECOND = -1; // -1 voor geen limiet
        public static readonly int DISPLAY_FPS = 1000; // Niet te hoog, anders loopt de UI thread vast
        public static readonly double fixedTimeCalls = 0.02; // Tijd tussen elke fixed time call. 

        // Grootte voor de canvas. Wordt automatisch geschaald naar scherm grootte. 
        public static readonly int CANVAS_WIDTH = 1280; 
        public static readonly int CANVAS_HEIGHT = 720; 
    }
}
