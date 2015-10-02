using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace DuckHunt.Controllers
{
    /// <summary>
    /// UI Controller class. 
    /// </summary>
    public class UI
    {
        private MainWindow _mainWindow;
        private Game _game;

        private static Dispatcher _dispatcher;

        private static UI _instance;

        public UI()
        {
            // Sla de UI thread dispatcher op
            _dispatcher = Dispatcher.CurrentDispatcher;

            // Maak de main window aan
            _mainWindow = new MainWindow();
            _mainWindow.MainCanvas.Width = CONSTANTS.CANVAS_WIDTH;
            _mainWindow.MainCanvas.Height = CONSTANTS.CANVAS_HEIGHT;
            _mainWindow.Show();

            // Registreer events
            _mainWindow.Closing += Window_Closing;
            _mainWindow.MainCanvas.MouseDown += MainCanvas_MouseDown;

            // Maak de gameController aan
            _game = new Game(this);
            _game.StartGame();

            _instance = this;
        }

        /// <summary>
        /// Als de window sluit, stop de gamethread.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_game != null)
            {
                _game.StopGame();
            }
        }

        /// <summary>
        /// Als er geklikt word, sla dit op. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MainCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Click events komen altijd van de UI thread, geen invoke nodig. 
            lock(Locks.ClickedPoints)
            {
                _game.InputContainer.ClickedPoints.Add(e.GetPosition(_mainWindow.MainCanvas));
            }
        }

        /// <summary>
        /// Slaat de huidige muispositie op in de inputContainer. Dit draait altijd op de UI thread. 
        /// </summary>
        public void UpdateMousePosition()
        {
            BeginInvoke(() =>
            {
                _game.InputContainer.MousePosition = Mouse.GetPosition(_mainWindow.MainCanvas);
            });
        }

        public void UpdateScreen(IGame game)
        {
            BeginInvoke(() => 
            {
                _mainWindow.FPS.Content = "FPS: " + game.FPS;
                game.UnitContainer.DrawAllUnits(game);
            });
        }

        public static void TryAddGraphics(UIElement element)
        {
            _instance.AddGraphics(element);
        }
        private void AddGraphics(UIElement element)
        {
            BeginInvoke(() =>
            {
                Console.WriteLine("Starting to add gfx");
                _mainWindow.MainCanvas.Children.Add(element);
            });
        }

        public static void TryRemoveGraphics(UIElement element)
        {
            _instance.RemoveGraphics(element);
        }
        private void RemoveGraphics(UIElement element)
        {
            BeginInvoke(() =>
            {
                Console.WriteLine("Starting to remove gfx");
                _mainWindow.MainCanvas.Children.Remove(element);
            });
        }

        /// <summary>
        /// Voert een functie uit op de UI thread.
        /// </summary>
        /// <param name="action">De actie die uitgevoerd moet worden.</param>
        public static void BeginInvoke(Action action)
        {
            // Als de huidige thread access heeft, is de dispatcher niet nodig.
            if (_dispatcher.CheckAccess())
                action.Invoke();
            else
                _dispatcher.BeginInvoke(action);
        }

        /// <summary>
        /// Voert een funcite synchroon uit op de UI thread, met return value.
        /// </summary>
        /// <typeparam name="T">Het type van de return value</typeparam>
        /// <param name="action">De actie die uitgevoerd moet worden</param>
        /// <returns>De waarde die de actie returned</returns>
        public static T Invoke<T>(Func<T> action)
        {
            if (_dispatcher.CheckAccess())
                return action.Invoke();
            else
                return _dispatcher.Invoke<T>(action);
        }
    }
}
