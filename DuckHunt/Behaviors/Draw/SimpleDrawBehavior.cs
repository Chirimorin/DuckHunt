using DuckHunt.Behaviors.Move;
using DuckHunt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DuckHunt.Behaviors.Draw
{
    class SimpleDrawBehavior : IDrawBehavior
    {
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

        private double Width
        {
            get
            {
                return Gfx.Width;
            }
            set
            {
                Gfx.Width = value;
            }
        }
        private double Height
        {
            get
            {
                return Gfx.Height;
            }
            set
            {
                Gfx.Height = value;
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

        public SimpleDrawBehavior(Canvas canvas)
        {
            Width = 20;
            Height = 20;

            Color = Brushes.Blue;
            Stroke = Brushes.Black;
            StrokeThickness = 2;

            try
            {
                canvas.Children.Add(Gfx);
            }
            catch (Exception ex)
            {
                Console.WriteLine("SimpleDrawBehavior error: " + ex.Message);
            }
        }

        public void Draw(IMoveBehavior behavior)
        {
            Canvas.SetLeft(Gfx, behavior.PosX);
            Canvas.SetTop(Gfx, behavior.PosY);
        }
    }
}
