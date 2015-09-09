using DuckHunt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
        private object _gameStateLock = new object();
        private bool _isRunning = false;

        #endregion

        /// <summary>
        /// Function to start the game loop.
        /// </summary>
        /// <returns>true if a new gameloop was started.</returns>
        public bool StartGameLoop()
        {
            lock(_gameStateLock)
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

        public void AbortGameLoop()
        {
            _isRunning = false;
            _gameLoopThread.Abort();
        }

        private Chicken _currentChicken;

        /// <summary>
        /// The main game loop. This should be started with StartGameLoop().
        /// </summary>
        private void GameLoop()
        {
            lock(_gameStateLock)
            {
                _isRunning = true;
            }

            _currentChicken = new Chicken();

            while (_isRunning)
            {
                HandleInputs();
                UpdateGame();
                UpdateScreen();

                Thread.Sleep(16);
            }

            ClearGame();
        }

        private void HandleInputs()
        {
            // TODO: collect and handle inputs;
        }

        private void UpdateGame()
        {
            // TODO: update all game objects. 
            _currentChicken.Move();
        }

        public event EventHandler UpdateScreenEvent;
        private void UpdateScreen()
        {
            if (UpdateScreenEvent != null)
            {
                UpdateScreenEvent(this, EventArgs.Empty);
            }
        }

        private void ClearGame()
        {
            _currentChicken = null;
        }


    }
}
