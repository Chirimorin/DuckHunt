using System;
using System.Collections.Generic;
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
            Time = DateTime.Now;
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

        private DateTime Time { get; set; }
        private double _deltaTime;
        public double DeltaTime
        {
            get
            {
                return _deltaTime;
            }
        }

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
            DateTime now = DateTime.Now;
            _deltaTime = ((now - Time).Milliseconds)/1000d;
            Time = now;
        }
    }
}
