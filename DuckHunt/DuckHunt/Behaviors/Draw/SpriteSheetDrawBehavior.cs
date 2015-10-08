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
        protected bool _addedToCanvas = false;

        private readonly int _frameCount;
        private readonly double _frameTime;
        private readonly bool _loop;
        private readonly BitmapSource[] _sprites;
        private readonly TransformGroup _transformRegular;
        private readonly TransformGroup _transformFlipped;

        private bool _animationCompleted;
        private int _currentFrame;
        private double _timePassed;

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
        
        public void Animate(Unit unit, IGame game)
        {
            if (_loop || !_animationCompleted)
            {
                _timePassed += game.DT;
                
                if (_timePassed > _frameTime)
                {
                    int frames = (int)Math.Floor(_timePassed / _frameTime);
                    _timePassed -= (frames * _frameTime);
                    _currentFrame += frames;

                    while (_loop && _currentFrame >= _frameCount)
                    {
                        _currentFrame -= _frameCount;
                    }

                    if (!_loop && _currentFrame >= _frameCount)
                    {
                        _currentFrame = _frameCount - 1;
                        _animationCompleted = true;
                    }
                }
            }
        }

        public void Draw(Unit unit, IGame game)
        {
            // Animate rekent currentFrame uit
            _gfx.Source = _sprites[_currentFrame];

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

        public void AddGfx(IGame game)
        {
            game.GraphicsContainer.AddGraphic(_gfx);
        }

        public void RemoveGfx(IGame game)
        {
            game.GraphicsContainer.RemoveGraphic(_gfx);
        }
    }
}
