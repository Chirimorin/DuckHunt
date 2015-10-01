using DuckHunt.Behaviors;
using DuckHunt.Behaviors.OldMove;
using DuckHunt.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

namespace DuckHunt.Model
{
    public abstract class Unit
    {
        // TODO: Dit moet niet hier, is alleen voor even omdat geen references bij properties...
        protected Random random;

        private readonly double _birthTime;

        public Unit(IGame game, double width, double height, double posX, double posY, double maxLifeTime)
        {
            random = game.Random;


            Width = width;
            Height = height;
            PosX = posX;
            PosY = posY;
            MaxLifetime = maxLifeTime;

            _birthTime = game.Time;
        }

        #region Behaviors
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
                    _moveBehavior.ThisUnit = null;
                }
                _moveBehavior = value;
                _moveBehavior.ThisUnit = this;
            }
        }

        /// <summary>
        /// De move behavior bepaald door de Unit zelf
        /// Key (string) = type
        /// Value (object[]) = constructor params voor die behavior
        /// </summary>
        public virtual KeyValuePair<string, object[]> PreferredMoveBehavior
        {
            get
            {
                return new KeyValuePair<string, object[]>("simple", null);
            }
        }

        //private BaseDrawBehavior _drawBehavior;

        public void FixedTimePassed(IGame game)
        {
            if (MoveBehavior != null)
            {
                MoveBehavior.FixedTimePassed(game);
            }
        }

        //public BaseDrawBehavior DrawBehavior
        //{
        //    get
        //    {
        //        return _drawBehavior;
        //    }
        //    set
        //    {
        //        if (_drawBehavior != null)
        //        {
        //            _drawBehavior.Parent = null;
        //        }
        //        _drawBehavior = value;
        //        _drawBehavior.Parent = this;
        //    }
        //}

        /// <summary>
        /// De draw behavior bepaald door de Unit zelf
        /// Key (string) = type
        /// Value (object[]) = constructor params voor die behavior
        /// </summary>
        public virtual KeyValuePair<string, object[]> PreferredDrawBehavior
        {
            get
            {
                return new KeyValuePair<string, object[]>("simple", null);
            }
        }
        #endregion

        #region Posities
        private double _posX;
        /// <summary>
        /// X-positie van de linker rand van de Unit (in pixels)
        /// </summary>
        public double PosX
        {
            get { return _posX; }
            set { _posX = value; }
        }
        /// <summary>
        /// X-positie van het midden van de Unit (in pixels)
        /// </summary>
        public double PosXMiddle
        {
            get { return PosX + (0.5 * Width); }
            set { PosX = value - (0.5 * Width); }
        }
        /// <summary>
        /// X-positie van de rechter rand van de Unit (in pixels)
        /// </summary>
        public double PosXRight
        {
            get { return PosX + Width; }
            set { PosX = value - Width; }
        }

        private double _posY;
        /// <summary>
        /// Y-positie van de bovenste rand van de Unit (in pixels)
        /// </summary>
        public double PosY
        {
            get { return _posY; }
            set { _posY = value; }
        }
        /// <summary>
        /// Y-positie van het midden van de Unit (in pixels)
        /// </summary>
        public double PosYMiddle
        {
            get { return PosY + (0.5 * Height); }
            set { PosY = value - (0.5 * Height); }
        }
        /// <summary>
        /// Y-positie van de onderste rand van de Unit (in pixels)
        /// </summary>
        public double PosYBottom
        {
            get { return PosY + Height; }
            set { PosY = value - Height; }
        }
        #endregion

        #region Afmetingen
        private double _width;
        /// <summary>
        /// Breedte van de Unit (in pixels)
        /// </summary>
        public double Width
        {
            get { return _width; }
            set
            {
                _width = value;
                //if (DrawBehavior != null)
                //    DrawBehavior.UpdateSize();
            }
        }

        private double _height;
        /// <summary>
        /// Hoogte van de Unit (in pixels)
        /// </summary>
        public double Height
        {
            get { return _height; }
            set
            {
                _height = value;
                //if (DrawBehavior != null)
                //    DrawBehavior.UpdateSize();
            }
        }
        #endregion

        #region Tijd zichtbaar
        /// <summary>
        /// Tijd dat het object in beeld blijft in seconden
        /// </summary>
        public double MaxLifetime { get; set; }
        #endregion

        private bool _isDestroyed = false;
        public bool IsDestroyed
        {
            get { return _isDestroyed; }
            private set { _isDestroyed = value; }
        }

        #region logica functies
        /// <summary>
        /// Controleert of een specifiek punt de Unit raakt
        /// </summary>
        /// <param name="point">Het punt om te controleren</param>
        /// <returns>true als deze Unit het punt raakt</returns>
        public virtual bool isHit(Point point)
        {
            //if (DrawBehavior != null)
            //{
            //    return (point.X > PosX &&
            //       point.X < PosX + Width &&
            //       point.Y > PosY &&
            //       point.Y < PosY + Height);
            //}

            return false;
        }

        /// <summary>
        /// Checkt of de tijd waarin deze Unit maximaal in beeld mag zijn, is verstreken
        /// </summary>
        /// <returns>true als deze Unit moet verdwijnen</returns>
        public virtual bool isMaxLifetimeExpired(IGame game)
        {
            return (game.Time - _birthTime) > MaxLifetime;
        }
        #endregion

        #region event functies
        /// <summary>
        /// Wordt aangeroepen bij elke muisklik
        /// </summary>
        /// <param name="point">Het punt waar geklikt is</param>
        public virtual void onClick(Point point) { }
        #endregion
        
        /// <summary>
        /// Verwijderd de unit uit het spel. 
        /// </summary>
        public virtual void destroy()
        {
            _isDestroyed = true;
        }

        public void Move(IGame game)
        {
            if (MoveBehavior != null)
            {
                MoveBehavior.Move(game);
            }
        }

        public void Draw(IGame game)
        {
            //if (DrawBehavior != null)
            //{
            //    DrawBehavior.Draw(game);
            //}
        }

        public void ClearGraphics()
        {
            //if (DrawBehavior != null)
            //{
            //    DrawBehavior.clearGraphics();
            //}
        }
    }
}
