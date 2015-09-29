using DuckHunt.Behaviors.Move;
using DuckHunt.Factories;
using DuckHunt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DuckHunt.Behaviors.Draw
{
    class SimpleDrawBehavior : BaseDrawBehavior
    {
        public static void RegisterSelf()
        {
            DrawBehaviorFactory.register("simple", typeof(SimpleDrawBehavior));
        }

        private Ellipse _gfx;
        private Ellipse Gfx
        {
            get
            {
                if (_gfx == null)
                {
                    _gfx = new Ellipse();
                }
                return _gfx;
            }
        }

        private Brush Color
        {
            get
            {
                return Gfx.Fill;
            }
            set
            {
                Gfx.Fill = value;
            }
        }
        private Brush Stroke
        {
            get
            {
                return Gfx.Stroke;
            }
            set
            {
                Gfx.Stroke = value;
            }
        }

        private double StrokeThickness
        {
            get
            {
                return Gfx.StrokeThickness;
            }
            set
            {
                Gfx.StrokeThickness = value;
            }
        }

        public SimpleDrawBehavior()
        {
            Color = Brushes.Blue;
            Stroke = Brushes.Black;
            StrokeThickness = 2;

            try
            {
                lock(Locks.DrawHelperContainer)
                {
                    OldDrawHelperContainer.Instance.Canvas.Children.Add(Gfx);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("SimpleDrawBehavior error: " + ex.Message);
            }
        }

        public override void Draw()
        {
            Canvas.SetLeft(Gfx, PosX);
            Canvas.SetTop(Gfx, PosY);
        }

        public override void UpdateSize()
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                Gfx.Width = Width;
                Gfx.Height = Height;
            });
        }

        public override void clearGraphics()
        {
            lock (Locks.DrawHelperContainer)
            {
                OldDrawHelperContainer.Instance.Canvas.Children.Remove(Gfx);
            }
        }
    }
}
