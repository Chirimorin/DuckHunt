using DuckHunt.Behaviors.Draw;
using DuckHunt.Behaviors.Move;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DuckHunt.Model
{
    public abstract class Unit
    {
        private BaseMoveBehavior _moveBehavior;
        public BaseMoveBehavior MoveBehavior
        {
            get
            {
                return _moveBehavior;
            }
            set
            {
                if (_moveBehavior != null)
                {
                    _moveBehavior.Parent = null;
                }
                _moveBehavior = value;
                _moveBehavior.Parent = this;
            }
        }

        private BaseDrawBehavior _drawBehavior;
        public BaseDrawBehavior DrawBehavior
        {
            get
            {
                return _drawBehavior;
            }
            set
            {
                if (_drawBehavior != null)
                {
                    _drawBehavior.Parent = null;
                }
                _drawBehavior = value;
                _drawBehavior.Parent = this;
            }
        }
        
        private double _posX;
        public double PosX
        {
            get
            {
                return _posX;
            }
            set
            {
                _posX = value;
            }
        }

        private double _posY;
        public double PosY
        {
            get
            {
                return _posY;
            }

            set
            {
                _posY = value;
            }
        }

        private double _width;
        public double Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
                if (DrawBehavior != null)
                    DrawBehavior.UpdateSize();
            }
        }

        private double _height;
        public double Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
                if (DrawBehavior != null)
                    DrawBehavior.UpdateSize();
            }
        }

        public virtual KeyValuePair<string, object[]> PreferredMoveBehavior
        {
            get
            {
                return new KeyValuePair<string, object[]>("simple", null);
            }
        }

        public virtual KeyValuePair<string, object[]> PreferredDrawBehavior
        {
            get
            {
                return new KeyValuePair<string, object[]>("simple", null);
            }
        }


        public virtual bool isHit(Point point)
        {
            if (DrawBehavior != null)
            {
                double minX = PosX;
                double maxX = PosX + Width;
                double minY = PosY;
                double maxY = PosY + Height;

                if (minX < point.X &&
                    maxX > point.X &&
                    minY < point.Y &&
                    maxY > point.Y)
                {
                    return true;
                }
            }

            return false;
        }

        public abstract void clicked(Point point);

        public virtual void init(double width, double height, double posX = 0, double posY = 0)
        {
            Width = width;
            Height = height;
            PosX = posX;
            PosY = posY;
        }
    }
}
