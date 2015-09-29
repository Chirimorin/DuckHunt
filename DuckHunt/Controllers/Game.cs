using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckHunt.Containers;
using System.Diagnostics;

namespace DuckHunt.Controllers
{
    public class Game : IGame
    {
        #region Lazy Singleton Implementation
        private static readonly Lazy<Game> _instance
            = new Lazy<Game>(() => new Game());

        private Game()
        {
            InputContainer = new InputContainer();
            UnitContainer = new UnitContainer();

            TickTime = 1.0 / Stopwatch.Frequency;
            Time = Stopwatch.GetTimestamp();
            // Calculate the minimum frame time. 
            //if (FPSlimit)
            //    minTicksPerFrame = Stopwatch.Frequency / maxFPS;
        }

        public static Game Instance
        {
            get { return _instance.Value; }
        }
        #endregion

        #region containers
        private InputContainer _inputContainer;
        public InputContainer InputContainer
        {
            get { return _inputContainer; }
            private set { InputContainer = value; }
        }

        private UnitContainer _unitContainer;
        public UnitContainer UnitContainer
        {
            get { return _unitContainer; }
            set { _unitContainer = value; }
        }
        #endregion

        private Random _random;
        public Random Random
        {
            get
            {
                if (_random == null)
                    _random = new Random();
                return _random;
            }
        }

        #region Time
        private double _dt;
        /// <summary>
        /// Frame tijd (in seconden)
        /// </summary>
        public double DT
        {
            get { return _dt; }
            private set { _dt = value; }
        }

        private double _fps;
        /// <summary>
        /// Frames per seconde
        /// </summary>
        public double FPS
        {
            get { return _fps; }
            set { _fps = value; }
        }

        private double _time;
        /// <summary>
        /// Totale speltijd (in seconden)
        /// </summary>
        public double Time
        {
            get { return _time; }
            set { _time = value; }
        }

        private readonly double TickTime;
        private long _totalTicks;
        private double _accumulator = 0;
        #endregion

        private void UpdateTime()
        {
            long ticks = Stopwatch.GetTimestamp();

            DT = (ticks - _totalTicks) * (TickTime);
            Time = ticks * TickTime;
            FPS = Math.Round(1d / DT, 1);
            _accumulator += DT;

            _totalTicks = ticks;
            
        }

    }
}
