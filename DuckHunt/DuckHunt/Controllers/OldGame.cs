using DuckHunt.Behaviors;
using DuckHunt.Factories;
using DuckHunt.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace DuckHunt.Controllers
{
    public class OldGame //: IGame
    {
        #region Lazy Singleton Implementation
        // The Lazy class guarantees Thread-safe lazy-construction of the object. 
        private static readonly Lazy<OldGame> _instance
            = new Lazy<OldGame>(() => new OldGame());

        private OldGame()
        {
            // Calculate the minimum frame time. 
            if (FPSlimit)
                minTicksPerFrame = Stopwatch.Frequency / maxFPS;
        }

        public static OldGame Instance
        {
            get
            {
                // Double-check locking and creation is handled by the Lazy class.
                return _instance.Value;
            }
        }
        #endregion

        #region Game loop management
        private Thread _gameLoopThread;
        private bool _isRunning = false;

        private readonly bool FPSlimit = true;
        private readonly int maxFPS = 60;
        private readonly long minTicksPerFrame = 1;
        private readonly double fixedTimeCalls = 0.02; // Roep FixedTimePassed() 50 keer per seconde aan.

        private double accumulator = 0; // Houd de tijd bij voor FixedTimePassed()
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

        /// <summary>
        /// Function to start the game loop.
        /// </summary>
        /// <returns>true if a new gameloop was started.</returns>
        public bool StartGame()
        {
            if (_isRunning)
            {
                // Game loop is already running. 
                return false;
            }

            _gameLoopThread = new Thread(new ThreadStart(GameLoop));
            _gameLoopThread.Start();

            return true;
        }

        public void StopGame()
        {
            lock (Locks.GameState)
            {
                _isRunning = false;
            }
        }

        /// <summary>
        /// The main game loop. This should be started with StartGameLoop().
        /// </summary>
        private void GameLoop()
        {
            lock (Locks.GameState)
            {
                _isRunning = true;
            }

            while (_isRunning)
            {
                UpdateTime();
                HandleInputs();
                UpdateGame();
                UpdateScreen();

                // Thread.Sleep en Thread.Yield zorgen voor haperen
                // Wait until we can start this frame. 
                while (!TimePassed()) ;
            }

            ClearGame();
        }

        /// <summary>
        /// Checks if the minimum frame time has passed since the last frame. 
        /// This is either 1 tick (smallest measurable time) or calculated based on the FPS limit. 
        /// </summary>
        /// <returns>True if the next frame can start.</returns>
        private bool TimePassed()
        {
            lock (Locks.ActionContainer)
            {
                return ((Stopwatch.GetTimestamp() - OldActionContainer.Instance.Time) > minTicksPerFrame);
            }
        }

        /// <summary>
        /// Updates the time to represent the time since the last call
        /// </summary>
        private void UpdateTime()
        {
            lock (Locks.ActionContainer)
            {
                OldActionContainer.Instance.updateTime();
                accumulator += OldActionContainer.Instance.DeltaTime;
            }
        }

        private void HandleInputs()
        {
            lock (Locks.InputContainer)
            lock (Locks.UnitContainer)
                {
                    System.Drawing.Point mousePosition = System.Windows.Forms.Control.MousePosition;
                    OldInputContainer.Instance.MousePosition = new Point(mousePosition.X, mousePosition.Y);

                    foreach (Point point in OldInputContainer.Instance.ClickedPoints)
                    {
                        OldUnitContainer.clicked(point);
                    }

                    OldInputContainer.Instance.ClickedPoints.Clear();
                }
        }

        private void UpdateGame()
        {
            lock (Locks.UnitContainer)
            {
                OldUnitContainer.RemoveDeadUnits();

                while (OldUnitContainer.NumUnits < 3)
                {
                    UnitFactory.Instance.createRandomUnit();
                }

                OldUnitContainer.MoveUnits();

                while (accumulator > fixedTimeCalls)
                {
                    OldUnitContainer.FixedTimePassed();
                    accumulator -= fixedTimeCalls;
                }
            }
        }

        private void UpdateScreen()
        {
            try
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    lock (Locks.UnitContainer)
                    {
                        OldUnitContainer.DrawUnits();
                    }

                    lock (Locks.ActionContainer)
                        lock (Locks.DrawHelperContainer)
                            OldDrawHelperContainer.Instance.FPS.Content = OldActionContainer.Instance.FPS + " FPS";
                });

            }
            catch { }

        }

        private void ClearGame()
        {
            lock (Locks.UnitContainer)
            {
                OldUnitContainer.RemoveAllUnits();
            }
        }

    }
}
