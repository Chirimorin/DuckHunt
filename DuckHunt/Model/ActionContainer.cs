using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt.Model
{
    /// <summary>
    /// This class is responsible for keeping all data that needs to be exchanged between threads like input and output. 
    /// </summary>
    class ActionContainer
    {
        #region Lazy Singleton Implementation
        // The Lazy class guarantees Thread-safe lazy-construction of the object. 
        private static readonly Lazy<ActionContainer> _instance
            = new Lazy<ActionContainer>(() => new ActionContainer());

        private ActionContainer()
        {
            Console.WriteLine("Frequency: " + Stopwatch.Frequency);
            // Frequency is door hardware bepaald. 
            TickTime = 1.0 / Stopwatch.Frequency;
            Time = Stopwatch.GetTimestamp();
        }

        public static ActionContainer Instance
        {
            get
            {
                // Double-check locking and creation is handled by the Lazy class.
                return _instance.Value;
            }
        }
        #endregion

        public long Time { get; private set; }
        public readonly double TickTime;
        //public double TickTime { get; private set; }
        public double DeltaTime { get; private set; }

        public double FPS
        {
            get
            {
                return Math.Round(1d / DeltaTime, 1);
            }
        }

        public double WindowWidth { get; set; }
        public double WindowHeight { get; set; }

        public void updateTime()
        {
            long now = Stopwatch.GetTimestamp();
            DeltaTime = (now - Time) * (TickTime);
            Time = now;
        }
    }
}
