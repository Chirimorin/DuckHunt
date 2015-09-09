using DuckHunt.Controllers;
using DuckHunt.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DuckHunt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += RecalculateScreenSize;
            SizeChanged += RecalculateScreenSize;

            GameController.Instance.StartGameLoop();
            GameController.Instance.UpdateScreenEvent += UpdateScreen;
        }

        private void RecalculateScreenSize(object sender, EventArgs e)
        {
            lock (ActionContainer.Instance)
            {
                ActionContainer.Instance.WindowWidth = MainCanvas.ActualWidth;
                ActionContainer.Instance.WindowHeight = MainCanvas.ActualHeight;
            }
        }

        private void UpdateScreen(object sender, EventArgs e)
        {
            // Invoke makes sure this runs on the UI thread as it should. 
            try
            {
                Dispatcher.Invoke(delegate
                {
                    lock (ActionContainer.Instance)
                    {
                        Chicken chicken = ActionContainer.Instance.MovedObjects.FirstOrDefault();

                        if (chicken.Gfx == null)
                        {
                            chicken.Gfx = new Ellipse();
                            chicken.Gfx.StrokeThickness = 2;
                            chicken.Gfx.Stroke = Brushes.Blue;
                            chicken.Gfx.Width = 20;
                            chicken.Gfx.Height = 20;

                            MainCanvas.Children.Add(chicken.Gfx);
                        }

                        Canvas.SetLeft(chicken.Gfx, chicken.PosX);
                        Canvas.SetTop(chicken.Gfx, chicken.PosY);
                    }
                });
            }
            catch
            {

            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GameController.Instance.AbortGameLoop();
        }
    }
}
