using DuckHunt.Behaviors;
using DuckHunt.Factories;
using DuckHunt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace DuckHunt.Controllers
{
    public class GameController
    {
        #region Lazy Singleton Implementation
        // The Lazy class guarantees Thread-safe lazy-construction of the object. 
        private static readonly Lazy<GameController> _instance
            = new Lazy<GameController>(() => new GameController());

        private GameController() { }

        public static GameController Instance
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
        public bool StartGameLoop()
        {
            lock (Locks.GameState)
            {
                if (_isRunning)
                {
                    // Game loop is already running. 
                    return false;
                }
            }

            _gameLoopThread = new Thread(new ThreadStart(GameLoop));
            _gameLoopThread.Start();

            return true;
        }

        public void StopGameLoop()
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
                HandleInputs();
                UpdateGame();
                UpdateScreen();

                Thread.Sleep(1);
                Thread.Yield();
            }

            ClearGame();
        }

        private void HandleInputs()
        {
            lock (Locks.InputContainer)
            lock (Locks.UnitContainer)
            {
                foreach (Point point in InputContainer.Instance.ClickedPoints)
                {
                    UnitContainer.clicked(point);
                }

                InputContainer.Instance.ClickedPoints.Clear();
            }
        }

        private void UpdateGame()
        {
            lock (Locks.ActionContainer)
            {
                ActionContainer.Instance.updateTime();
            }

            lock (Locks.UnitContainer)
            {
                UnitContainer.RemoveDeadUnits();

                while (UnitContainer.NumUnits < 3)
                {
                    UnitFactory.Instance.createUnit("chicken");
                }
                
                UnitContainer.MoveUnits();
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
                        UnitContainer.DrawUnits();
                    }

                    lock (Locks.ActionContainer)
                        lock (Locks.DrawHelperContainer)
                            DrawHelperContainer.Instance.FPS.Content = ActionContainer.Instance.FPS + " FPS";
                });

            }
            catch { }
            
        }

        private void ClearGame()
        {
            lock (Locks.UnitContainer)
            {
                UnitContainer.RemoveAllUnits();
            }
        }

    }
}
