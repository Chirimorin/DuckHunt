﻿using System;
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
using DuckHunt.Units.Chicken;
using DuckHunt.Units.Bunny;
using DuckHunt.Units.Common;
using DuckHunt.Behaviors.Move;
using DuckHunt.Behaviors.Move.Flying;
using DuckHunt.Behaviors.Move.Common;
using DuckHunt.Behaviors.Move.Running;
using DuckHunt.Behaviors.Draw;
using DuckHunt.Levels;

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
            private set { _unitContainer = value; }
        }

        private GraphicsContainer _graphicsContainer;
        public GraphicsContainer GraphicsContainer
        {
            get { return _graphicsContainer; }
            private set { _graphicsContainer = value; }
        }
        #endregion

        #region Level
        private ILevel _currentLevel;
        /// <summary>
        /// The current level
        /// </summary>
        public ILevel CurrentLevel
        {
            get { return _currentLevel; }
            set { _currentLevel = value; }
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
        /// Update tijd (in seconden)
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
        private long _ticksSinceDraw;

        private double _accumulator;
        #endregion

        #region Game loop info
        private Thread _gameLoopThread;
        private bool _isRunning = false;
        public bool IsRunning { get { return _isRunning; } }

        private readonly long minTicksPerUpdate = 1;
        private readonly long minTicksPerDraw = 1;
        #endregion

        public int CurrentScore { get; private set; }

        // --------------------------------------------------------- CTOR --------------------------------------------------------- //

        public Game(UI ui)
        {
            _ui = ui;

            // Readonly velden instellen. 
            _tickTime = 1.0 / Stopwatch.Frequency;

            // Bereken de minimale frame tijd
            if (CONSTANTS.UPDATES_PER_SECOND > 0)
                minTicksPerUpdate = Stopwatch.Frequency / CONSTANTS.UPDATES_PER_SECOND;

            if (CONSTANTS.DISPLAY_FPS > 0)
                minTicksPerDraw = Stopwatch.Frequency / CONSTANTS.DISPLAY_FPS;



            // Factory registraties
            #region Units
            UnitFactories.Units.Register("chicken", () => new Chicken("chicken"));
            UnitFactories.Units.Register("bunny", () => new Bunny("bunny"));
            #endregion

            #region UnitStates
            // Shared states
            UnitFactories.States.Register(default(string), "dead", () => new DeadUnitState("dead"));
            UnitFactories.States.Register(default(string), "endlevel", () => new DeadUnitState("endlevel", 0.5, 0.5));

            // Chicken states
            UnitFactories.States.Register("chicken", "alive", () => new AliveUnitState("alive"));
            UnitFactories.States.Register("chicken", "fleeing", () => new FleeingUnitState("fleeing"));

            // Bunny states
            UnitFactories.States.Register("bunny", "alive", () => new AliveBunnyState("alive"));
            UnitFactories.States.Register("bunny", "fleeing", () => new FleeingBunnyState("fleeing"));
            #endregion

            #region Unit Move Behaviors
            // Shared move behaviors
            UnitFactories.MoveBehaviors.Register(default(string), "dead", () => new DeadUnitMoveBehavior(900, 500));
            UnitFactories.MoveBehaviors.Register(default(string), "endlevel", () => new DeadUnitMoveBehavior(900, 500));

            // Chicken move behaviors
            UnitFactories.MoveBehaviors.Register("chicken","alive", () => new RandomFlightMoveBehavior(900, 900, 750, 750));
            UnitFactories.MoveBehaviors.Register("chicken","fleeing", () => new FlyingFleeMoveBehavior(1000, -100, 750, 500));

            // Bunny move behaviors
            UnitFactories.MoveBehaviors.Register("bunny", "alive", () =>
            {
                if (Random.Next(2) == 0) // 50% kans op jump over mouse behavior
                    return new JumpOverMouseMoveBehavior(0, 900, 1000, 500, 400, 0);
                return new GravityMoveBehavior(0, 900, 1000, 500, 400, 0, 100);
            });
            UnitFactories.MoveBehaviors.Register("bunny", "fleeing", () => new RunningFleeMoveBehavior(0, 900, 500, 500, 400, 0));
            #endregion

            #region Unit Draw Behaviors
            // Chicken draw behaviors
            UnitFactories.DrawBehaviors.Register("chicken", "alive", () => new SpriteSheetDrawBehavior("ChickenFly.png", 0.07, 0, true));
            UnitFactories.DrawBehaviors.Register("chicken", "fleeing", () => new SpriteSheetDrawBehavior("ChickenFly.png", 0.07, 0, true));
            UnitFactories.DrawBehaviors.Register("chicken", "dead", () => new SpriteSheetDrawBehavior("ChickenDead.png", 0.07, 12.5, true));
            UnitFactories.DrawBehaviors.Register("chicken", "endlevel", () => new SpriteSheetDrawBehavior("ChickenDead.png", 0.07, 12.5, true));

            // Bunny draw behaviors
            UnitFactories.DrawBehaviors.Register("bunny", "alive", () => new SpriteSheetDrawBehavior("BunnyRun.png", 0.075, 0, true));
            UnitFactories.DrawBehaviors.Register("bunny", "fleeing", () => new SpriteSheetDrawBehavior("BunnyRun.png", 0.075, 0, true));
            UnitFactories.DrawBehaviors.Register("bunny", "jumping", () => new SpriteSheetDrawBehavior("BunnyJump.png", 0.15, 0, false));
            UnitFactories.DrawBehaviors.Register("bunny", "dead", () => new SpriteSheetDrawBehavior("BunnyDead.png", 0.1, 0, false));
            UnitFactories.DrawBehaviors.Register("bunny", "endlevel", () => new SpriteSheetDrawBehavior("BunnyDead.png", 0.1, 0, false));
            #endregion
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
            GraphicsContainer = new GraphicsContainer();

            CurrentScore = 0;
            LevelFactory.Instance.NewGame(this);

            // Begintijd vaststellen
            UpdateTime();
            // Accumulator moet 0 zijn hier, andere waarden worden meteen overschreven.
            _accumulator = 0;
            _ticksSinceDraw = minTicksPerDraw;
            int updatesSinceDraw = 0;

            while (_isRunning)
            {
                UpdateTime();
                updatesSinceDraw++;

                HandleInputs();
                UpdateGame();

                if (_ticksSinceDraw > minTicksPerDraw)
                {
                    UpdateScreen(1.0/(_ticksSinceDraw * _tickTime), updatesSinceDraw);
                    _ticksSinceDraw = 0;
                    updatesSinceDraw = 0;
                }
                
                // Thread.Sleep zorgt voor haperen
                // Yield tot de minimale tijd voorbij is
                Thread.Yield();
                while (!TimePassed())
                {
                    Thread.Yield();
                };
            }

            // Update het scherm nog 1 keer na einde game (voor gameover scherm)
            UpdateScreen(0, 0);
        }

        /// <summary>
        /// Controleert of de minimum frame tijd voorbij is
        /// </summary>
        /// <returns>true als de volgende frame kan starten</returns>
        private bool TimePassed()
        {
            return ((Stopwatch.GetTimestamp() - _totalTicks) > minTicksPerUpdate);
        }

        /// <summary>
        /// Update alle tijd waarden voor de huidige frame.
        /// </summary>
        private void UpdateTime()
        {
            long ticks = Stopwatch.GetTimestamp();

            DT = (ticks - _totalTicks) * (_tickTime);
            Time = ticks * _tickTime;
            FPS = Math.Round(1.0 / DT, 0);

            _ticksSinceDraw += (ticks - _totalTicks);

            _accumulator += DT;

            _totalTicks = ticks;
        }

        /// <summary>
        /// Verwerkt user input
        /// </summary>
        private void HandleInputs()
        {
            if (_ticksSinceDraw > minTicksPerDraw)
            {
                _ui.UpdateMousePosition(); // Dispatcht zichzelf naar de UI thread.
            }

            InputContainer.HandleInputs(this);
        }

        /// <summary>
        /// Update de game status. 
        /// </summary>
        private void UpdateGame()
        {
            UnitContainer.ClearDestroyedUnits();

            CurrentScore += InputContainer.EarnedScore;

            CurrentLevel.Update(this);

            UnitContainer.UpdateAllUnits(this);

            while(_accumulator > CONSTANTS.fixedTimeCalls)
            {
                UnitContainer.FixedTimePassed(this);
                _accumulator -= CONSTANTS.fixedTimeCalls;
            }

            string[] allowedUnits = CurrentLevel.GetAllowedUnits(this);
            if (allowedUnits.Length > 0)
            {
                UnitFactories.Units
                    .Create(allowedUnits[Random.Next(allowedUnits.Length)])
                    .init(this);
            }

            UnitContainer.AnimateAllUnits(this);
        }

        /// <summary>
        /// Update de elementen op het scherm
        /// </summary>
        private void UpdateScreen(double fps, int updatesSinceDraw)
        {
            _ui.UpdateScreen(this, fps, updatesSinceDraw); // Dispatcht zichzelf naar de UI thread.
        }
    }
}
