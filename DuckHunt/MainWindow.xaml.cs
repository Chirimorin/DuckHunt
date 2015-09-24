using DuckHunt.Behaviors;
using DuckHunt.Factories;
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

            lock(Locks.DrawHelperContainer)
            {
                DrawHelperContainer.Instance.Canvas = MainCanvas;
                DrawHelperContainer.Instance.FPS = FPS;
            }

            GameController.Instance.StartGameLoop();
        }

        private void RecalculateScreenSize(object sender, EventArgs e)
        {
            lock (Locks.ActionContainer)
            {
                ActionContainer.Instance.WindowWidth = MainCanvas.ActualWidth;
                ActionContainer.Instance.WindowHeight = MainCanvas.ActualHeight;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GameController.Instance.StopGameLoop();
        }

        private void MainCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("click...");
            lock (Locks.InputContainer)
            {
                InputContainer.Instance.ClickedPoints.Add(e.GetPosition(MainCanvas));
            }
        }
    }
}
