using DuckHunt.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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

        private readonly ImageBrush[] _backgrounds;
        private int _currentBackground;

        public UI()
        {
            // Sla de UI thread dispatcher op
            _dispatcher = Dispatcher.CurrentDispatcher;

            // Laad alle achtergronden in
            List<ImageBrush> backgrounds = new List<ImageBrush>();
            for (int i = 1; i <= 7; i++)
            {
                backgrounds.Add(new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Backgrounds/" + i + ".png"))));
            }
            _backgrounds = backgrounds.ToArray();

            // Maak de main window aan
            _mainWindow = new MainWindow();
            _mainWindow.MainCanvas.Width = CONSTANTS.CANVAS_WIDTH;
            _mainWindow.MainCanvas.Height = CONSTANTS.CANVAS_HEIGHT;
            _mainWindow.Show();

            // Registreer events
            _mainWindow.Closing += Window_Closing;
            _mainWindow.MainCanvas.MouseDown += MainCanvas_MouseDown;
            _mainWindow.NewGame.Click += NewGame_Click;
            _mainWindow.Exit.Click += Exit_Click;

            // Maak de gameController aan
            _game = new Game(this);

            _instance = this;
            setBackground(-1);
        }

        private void setBackground(int level)
        {
            if (level != _currentBackground)
            {
                _currentBackground = level;
                ImageBrush img;

                if (level < 0)
                {   // Geen level (voor begin, na game over), random achtergrond.
                    img = _backgrounds[_game.Random.Next(_backgrounds.Length)];
                }
                else
                {   // Loop door de achtergronden heen
                    img = _backgrounds[level % _backgrounds.Length];
                }

                BeginInvoke(() =>
                {
                    _mainWindow.MainCanvas.Background = img;
                });
            }
        }

        /// <summary>
        /// Start een nieuw spel met de new game knop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            if (_game != null)
                _game.StartGame();
        }

        /// <summary>
        /// Sluit het spel af
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.Close();
        }

        /// <summary>
        /// Als de window sluit, stop de gamethread.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_game != null)
                _game.StopGame();
        }

        /// <summary>
        /// Als er geklikt word, sla dit op. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MainCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_game.IsRunning)
                _game.InputContainer.AddClick(e.GetPosition(_mainWindow.MainCanvas));
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
            string fps = (game.IsRunning ? "FPS: " + game.FPS : "");
            string currentlevel = game.CurrentLevel.Name;
            int shotsLeft = game.CurrentLevel.ShotsLeft;
            string score = "Score: " + game.CurrentScore + "\nSchoten over: ";
            if (shotsLeft == -1)
                score += "eindeloos";
            else
                score += shotsLeft;

            string bigText = game.CurrentLevel.BigText;

            setBackground(game.CurrentLevel.Level);

            BeginInvoke(() => 
            {
                _mainWindow.FPS.Content = fps;
                _mainWindow.CurrentLevel.Content = currentlevel;
                _mainWindow.CurrentScore.Content = score;
                _mainWindow.BigText.Text = bigText;
                _mainWindow.NewGame.Visibility = (game.IsRunning ? Visibility.Collapsed : Visibility.Visible);
                _mainWindow.Exit.Visibility = (game.IsRunning ? Visibility.Collapsed : Visibility.Visible);

                game.GraphicsContainer.UpdateGraphics(_mainWindow.MainCanvas);

                game.UnitContainer.DrawAllUnits(game);
            });
        }

        /// <summary>
        /// Voert een functie uit op de UI thread.
        /// </summary>
        /// <param name="action">De actie die uitgevoerd moet worden.</param>
        public void BeginInvoke(Action action)
        {
            // Als de huidige thread access heeft, is de dispatcher niet nodig.
            if (_dispatcher.CheckAccess())
                action.Invoke();
            else
                _dispatcher.BeginInvoke(action);
        }
    }
}
