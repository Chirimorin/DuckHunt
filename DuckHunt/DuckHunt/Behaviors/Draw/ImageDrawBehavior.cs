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
using System.Windows.Media.Imaging;

namespace DuckHunt.Behaviors.Draw
{
    public class ImageDrawBehavior : BaseDrawBehavior
    {
        public static void RegisterSelf()
        {
            DrawBehaviorFactory.register("image", typeof(ImageDrawBehavior));
        }

        private Image _gfx;
        public override UIElement Gfx
        {
            get
            {
                return _gfx;
            }
        }

        public ImageDrawBehavior(string filename)
        {
            _gfx = new Image();
            _gfx.Source = new BitmapImage(new Uri(@"\Images\" + filename, UriKind.Relative));

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
                _gfx.Width = Width;
                _gfx.Height = Height;
            });
        }
    }
}
