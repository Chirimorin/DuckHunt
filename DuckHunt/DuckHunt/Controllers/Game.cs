using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckHunt.Containers;
using System.Diagnostics;
using System.Threading;
using System.Windows.Threading;
using DuckHunt.Factories;
using DuckHunt.Units;

namespace DuckHunt.Controllers
{
    public class Game : IGame
    {
        // ------------------------------------------------------ Variabelen ------------------------------------------------------ //

        private UI _ui;

        #region Containers
        private InputContainer _inputContainer;
        public InputContainer InputContainer
        {
            get { return _inputContainer; }
            private set { _inputContainer = value; }
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

        private readonly double _tickTime;
        private long _totalTicks;

        private double _accumulator;
        #endregion

        #region Game loop info
        private Thread _gameLoopThread;
        private bool _isRunning = false;

        private readonly long minTicksPerFrame = 1;
        #endregion

        public int CurrentScore { get; private set; }

        // --------------------------------------------------------- CTOR --------------------------------------------------------- //

        public Game(UI ui)
        {
            _ui = ui;

            // Readonly velden instellen. 
            _tickTime = 1.0 / Stopwatch.Frequency;

            // Bereken de minimale frame tijd
            if (CONSTANTS.FPS_LIMIT > 0)
                minTicksPerFrame = Stopwatch.Frequency / CONSTANTS.FPS_LIMIT;
        }

        // ------------------------------------------------------- Functies ------------------------------------------------------- //

        #region Start/Stop game
        /// <summary>
        /// Start de game loop. 
        /// </summary>
        /// <returns>true als de gameloop gestart is, false als deze al draaide.</returns>
        public bool StartGame()
        {
            if (_isRunning)
            {
                // Game loop draait al, moet niet nog eens starten.
                return false;
            }

            _gameLoopThread = new Thread(new ThreadStart(GameLoop));
            _gameLoopThread.Start();

            return true;
        }

        /// <summary>
        /// Stopt de gameloop
        /// </summary>
        public void StopGame()
        {
            _isRunning = false;
        }
        #endregion

        /// <summary>
        /// De gameloop. StartGame() start deze loop (in een nieuwe thread)
        /// </summary>
        private void GameLoop()
        {
            _isRunning = true;

            // Nieuwe containers aanmaken aan het begin van het spel. 
            InputContainer = new InputContainer();
            UnitContainer = new UnitContainer();

            CurrentScore = 0;
            LevelFactory.Instance.NewGame(this);

            // Begintijd vaststellen
            UpdateTime();
            // Accumulator moet 0 zijn hier, andere waarden worden meteen overschreven.
            _accumulator = 0;

            while (_isRunning)
            {
                UpdateTime();
                HandleInputs();
                UpdateGame();
                UpdateScreen();

                // Thread.Sleep en Thread.Yield zorgen voor haperen
                // Wacht met een busy loop.
                while (!TimePassed()) ;
            }
        }

        /// <summary>
        /// Controleert of de minimum frame tijd voorbij is
        /// </summary>
        /// <returns>true als de volgende frame kan starten</returns>
        private bool TimePassed()
        {
            return ((Stopwatch.GetTimestamp() - _totalTicks) > minTicksPerFrame);
        }

        /// <summary>
        /// Update alle tijd waarden voor de huidige frame.
        /// </summary>
        private void UpdateTime()
        {
            long ticks = Stopwatch.GetTimestamp();

            DT = (ticks - _totalTicks) * (_tickTime);
            Time = ticks * _tickTime;
            FPS = Math.Round(1d / DT, 1);

            _accumulator += DT;

            _totalTicks = ticks;
        }

        /// <summary>
        /// Verwerkt user input
        /// </summary>
        private void HandleInputs()
        {
            _ui.UpdateMousePosition(); // Dispatcht zichzelf naar de UI thread.

            InputContainer.HandleInputs(this);
        }

        /// <summary>
        /// Update de game status. 
        /// </summary>
        private void UpdateGame()
        {
            CurrentScore += InputContainer.EarnedScore;

            LevelFactory.Instance.CurrentLevel.Update(this);

            UnitContainer.UpdateAllUnits(this);

            Unit newUnit = LevelFactory.Instance.CurrentLevel.TryCreateUnit(this);
            if (newUnit != null)
            {
                newUnit.BirthTime = Time;
                UnitContainer.AddUnit(newUnit);
            }

            while(_accumulator > CONSTANTS.fixedTimeCalls)
            {
                UnitContainer.FixedTimePassed(this);
                _accumulator -= CONSTANTS.fixedTimeCalls;
            }

            UnitContainer.ClearDestroyedUnits();
        }

        /// <summary>
        /// Update de elementen op het scherm
        /// </summary>
        private void UpdateScreen()
        {
            _ui.UpdateScreen(this); // Dispatcht zichzelf naar de UI thread.
        }
    }
}
