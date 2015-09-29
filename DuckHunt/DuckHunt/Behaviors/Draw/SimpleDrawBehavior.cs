using DuckHunt.Behaviors.Move;
using DuckHunt.Controllers;
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

        public override UIElement Gfx
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
                return ((Ellipse)Gfx).Fill;
            }
            set
            {
                ((Ellipse)Gfx).Fill = value;
            }
        }
        private Brush Stroke
        {
            get
            {
                return ((Ellipse)Gfx).Stroke;
            }
            set
            {
                ((Ellipse)Gfx).Stroke = value;
            }
        }

        private double StrokeThickness
        {
            get
            {
                return ((Ellipse)Gfx).StrokeThickness;
            }
            set
            {
                ((Ellipse)Gfx).StrokeThickness = value;
            }
        }

        public SimpleDrawBehavior()
        {
            Color = Brushes.Blue;
            Stroke = Brushes.Black;
            StrokeThickness = 2;

            TryAddGraphics();
        }

        public override void Draw(IGame game)
        {
            Canvas.SetLeft(Gfx, PosX);
            Canvas.SetTop(Gfx, PosY);
        }

        public override void UpdateSize()
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                ((Ellipse)Gfx).Width = Width;
                ((Ellipse)Gfx).Height = Height;
            });
        }
    }
}
