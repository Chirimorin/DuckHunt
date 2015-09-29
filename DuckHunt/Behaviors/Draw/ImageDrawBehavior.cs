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
        private Image Gfx
        {
            get
            {
                return _gfx;
            }
            set
            {
                _gfx = value;
            }
        }

        public ImageDrawBehavior(string filename)
        {
            Gfx = new Image();
            Gfx.Source = new BitmapImage(new Uri(@"\Images\" + filename, UriKind.Relative));

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
            Application.Current.Dispatcher.Invoke(delegate
            {
                lock (Locks.DrawHelperContainer)
                {
                    OldDrawHelperContainer.Instance.Canvas.Children.Remove(Gfx);
                }
            });
        }
    }
}
