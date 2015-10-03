using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DuckHunt.Controllers;
using DuckHunt.Units;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;
using System.Reflection;
using DuckHunt.Factories;

namespace DuckHunt.Behaviors.Draw
{
    public class SpriteSheetDrawBehavior : IDrawBehavior
    {
        private Image _gfx;
        public UIElement Gfx
        {
            get { return _gfx; }
        }

        private readonly int _frameCount;
        private readonly double _frameTime;
        private readonly bool _loop;
        private readonly BitmapSource[] _sprites;
        private readonly TransformGroup _transformRegular;
        private readonly TransformGroup _transformFlipped;

        private bool _animationCompleted;
        private int _currentFrame;
        private double _timePassed;

        public SpriteSheetDrawBehavior(string filename, int xImages, int yImages, int spriteWidth, int spriteHeight, double frameTime, double angle, bool loop)
        {
            // Voorbereiding
            _loop = loop;
            _frameTime = frameTime;
            _frameCount = xImages * yImages;
            _currentFrame = 0;
            _timePassed = 0;
            _transformRegular = new TransformGroup();
            _transformRegular.Children.Add(new ScaleTransform(1, 1));
            _transformRegular.Children.Add(new RotateTransform(angle));

            _transformFlipped = new TransformGroup();
            _transformFlipped.Children.Add(new ScaleTransform(-1, 1));
            _transformFlipped.Children.Add(new RotateTransform(-angle));

            _sprites = SpriteSheetFactory.Instance.ProcessSpriteSheet(filename, xImages, yImages);

            _gfx = new Image();
            _gfx.Source = _sprites[0];
            _gfx.RenderTransformOrigin = new Point(0.5, 0.5);
            _gfx.RenderTransform = _transformRegular;
        }

        public SpriteSheetDrawBehavior(string fileName, double frameTime, double angle, bool loop)
        {
            // Beginstate voor non-readonly variabelen
            Reset();

            // Sprites ophalen
            _sprites = SpriteSheetFactory.Instance.GetSprites(fileName);

            // Readonly variabelen
            _frameCount = _sprites.Length;
            _frameTime = frameTime;
            _loop = loop;

            // Transformgroups aanmaken (voor rechtzetten en spiegelen van de sprites)
            _transformRegular = new TransformGroup();
            _transformRegular.Children.Add(new ScaleTransform(1, 1));
            _transformRegular.Children.Add(new RotateTransform(angle));

            _transformFlipped = new TransformGroup();
            _transformFlipped.Children.Add(new ScaleTransform(-1, 1));
            _transformFlipped.Children.Add(new RotateTransform(-angle));

            // Graphics object aanmaken
            _gfx = new Image();
            _gfx.Source = _sprites[0];
            _gfx.RenderTransformOrigin = new Point(0.5, 0.5);
            _gfx.RenderTransform = _transformRegular;
        }

        public void Draw(Unit unit, IGame game)
        {
            if (_loop || !_animationCompleted)
            {
                _timePassed += game.DT;

                // Update sprites when needed. Even for low framerates
                while (_timePassed > _frameTime)
                {
                    _gfx.Source = _sprites[_currentFrame++];
                    _timePassed -= _frameTime;

                    if (_currentFrame == _frameCount)
                    {
                        if (_loop)
                            _currentFrame = 0;
                        else
                            _animationCompleted = true;
                    }
                }
            }

            if (unit.VX < 0)
                _gfx.RenderTransform = _transformFlipped;
            else
                _gfx.RenderTransform = _transformRegular;

            _gfx.Width = unit.Width;
            _gfx.Height = unit.Height;
            Canvas.SetLeft(_gfx, unit.PosX);
            Canvas.SetTop(_gfx, unit.PosY);
        }

        public void Reset()
        {
            _animationCompleted = false;
            _currentFrame = 0;
            _timePassed = 0;
        }
    }
}
