using DuckHunt.Controllers;
using DuckHunt.Factories;
using DuckHunt.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DuckHunt.Behaviors.Draw
{
    class SpriteSheetDrawBehavior : BaseDrawBehavior
    {
        public static void RegisterSelf()
        {
            DrawBehaviorFactory.register("spritesheet", typeof(SpriteSheetDrawBehavior));
        }

        private Image _gfx;
        public override UIElement Gfx
        {
            get { return _gfx; }
        }

        private BitmapSource[] _sprites;
        /// <summary>
        /// Houd alle losse sprites bij
        /// </summary>
        public BitmapSource[] Sprites
        {
            get { return _sprites; }
            set { _sprites = value; }
        }

        private int _frameCount;
        private int _currentFrame;
        private double _timePassed;
        private readonly double _frameTime;

        ScaleTransform _transformRegular;
        ScaleTransform _transformFlipped;

        public SpriteSheetDrawBehavior(string filename, int xImages, int yImages, int spriteWidth, int spriteHeight, double frameTime)
        {
            // Voorbereiding
            _frameTime = frameTime;
            _frameCount = xImages * yImages;
            _currentFrame = 0;
            _timePassed = 0;
            Sprites = new BitmapSource[_frameCount];
            _transformRegular = new ScaleTransform(1, 1);
            _transformFlipped = new ScaleTransform(-1, 1);


            // Rechthoek aanmaken ter grootte van de sprites
            System.Drawing.Rectangle cropRect = new System.Drawing.Rectangle(0, 0, spriteWidth, spriteHeight);

            // Spritesheet inladen
            System.Drawing.Bitmap src = System.Drawing.Image.FromFile(Path.GetFullPath(@"Images\" + filename)) as System.Drawing.Bitmap;
            System.Drawing.Bitmap target;

            for (int row = 0; row < xImages; row++)
            {
                for (int col = 0; col < yImages; col++)
                {
                    // Zoek de positie van de huidige sprite
                    int currentX = row * spriteWidth;
                    int currentY = col * spriteHeight;
                    cropRect.X = currentX;
                    cropRect.Y = currentY;

                    // Maak een bitmap om de losse sprite op te tekenen
                    target = new System.Drawing.Bitmap(cropRect.Width, cropRect.Height);

                    // Snij de sprite uit
                    using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(target))
                    {
                        g.DrawImage(src, new System.Drawing.Rectangle(0, 0, target.Width, target.Height), cropRect, System.Drawing.GraphicsUnit.Pixel);
                    }

                    // Zet de sprite om in een BitmapSource
                    BitmapSource frame = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(target.GetHbitmap(), IntPtr.Zero,
                    System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(target.Width, target.Height));

                    // Sla de sprite op in de array van sprites
                    int index = row + col * xImages;
                    Sprites[index] = frame;
                }
            }

            _gfx = new Image();
            _gfx.Source = Sprites[0];
            _gfx.RenderTransformOrigin = new Point(0.5, 0.5);
            _gfx.RenderTransform = _transformRegular;

            TryAddGraphics();
        }

        public override void Draw(IGame game)
        {
            _timePassed += game.DT;

            // Update sprites when needed. Even for low framerates
            while (_timePassed > _frameTime)
            {
                _gfx.Source = Sprites[_currentFrame++];
                _timePassed -= _frameTime;

                if (_currentFrame == _frameCount)
                    _currentFrame = 0;
            }

            if (Parent.MoveBehavior.VX < 0)
                _gfx.RenderTransform = _transformFlipped;
            else
                _gfx.RenderTransform = _transformRegular;

            Canvas.SetLeft(_gfx, PosX);
            Canvas.SetTop(_gfx, PosY);
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
