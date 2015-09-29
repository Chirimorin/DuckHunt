﻿using DuckHunt.Behaviors;
using DuckHunt.Behaviors.Draw;
using DuckHunt.Behaviors.Move;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

namespace DuckHunt.Model
{
    public abstract class Unit
    {
        private Stopwatch stopwatch = new Stopwatch();

        private readonly double _birthTime;
        /// <summary>
        /// De leeftijd van de unit, in seconden. 
        /// </summary>
        public double LifeTime
        {
            get
            {
                lock(Locks.ActionContainer)
                {
                    return OldActionContainer.Instance.TimeSeconds - _birthTime;
                }
            }
        }

        public Unit(double width, double height, double posX, double posY, double maxLifeTime)
        {
            Width = width;
            Height = height;
            PosX = posX;
            PosY = posY;
            MaxLifetime = maxLifeTime;

            lock (Locks.ActionContainer)
            {
                _birthTime = OldActionContainer.Instance.TimeSeconds;
            }
            stopwatch.Start();
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
                if (DrawBehavior != null)
                    DrawBehavior.UpdateSize();
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
                if (DrawBehavior != null)
                    DrawBehavior.UpdateSize();
            }
        }
        #endregion

        #region Tijd zichtbaar
        /// <summary>
        /// Tijd dat het object in beeld blijft in milliseconden
        /// </summary>
        public int MaxTimeVisable { get; set; }
        public double MaxLifetime { get; set; }
        #endregion

        #region logica functies
        /// <summary>
        /// Controleert of een specifiek punt de Unit raakt
        /// </summary>
        /// <param name="point">Het punt om te controleren</param>
        /// <returns>true als deze Unit het punt raakt</returns>
        public virtual bool isHit(Point point)
        {
            if (DrawBehavior != null)
            {
                return (point.X > PosX &&
                   point.X < PosX + Width &&
                   point.Y > PosY &&
                   point.Y < PosY + Height);
            }

            return false;
        }

        /// <summary>
        /// Checkt of de tijd waarin deze Unit maximaal in beeld mag zijn, is verstreken
        /// </summary>
        /// <returns>true als deze Unit moet verdwijnen</returns>
        public virtual bool isMaxLifetimeExpired()
        {
            return LifeTime > MaxLifetime;
        }

        /// <summary>
        /// Checkt of deze Unit uit het beeld is verdwenen en verwijderd deze Unit als dat zo is
        /// </summary>
        public virtual void removeWhenDisappeared()
        {
            if (PosX < 0 || PosX > MoveBehavior.WindowWidth || PosY < 0 || PosY > MoveBehavior.WindowHeight)
            {
                OldUnitContainer.RemoveUnit(this);
            }
        }
        #endregion

        #region event functies
        /// <summary>
        /// Wordt aangeroepen bij elke muisklik
        /// </summary>
        /// <param name="point">Het punt waar geklikt is</param>
        public virtual void onClick(Point point) { }
        #endregion

        public virtual void init(double width = double.NaN, double height = double.NaN, double posX = double.NaN, double posY = double.NaN, double maxLifeTime = double.PositiveInfinity)
        {
            // Waarom niet constructor? Weet ik ook niet... ;)
        }

        private bool _isDestroyed = false;
        /// <summary>
        /// Verwijderd de unit uit het spel. 
        /// </summary>
        public virtual void destroy()
        {
            if (!_isDestroyed)
            {
                _isDestroyed = true;
                //lock (Locks.UnitContainer)
                {
                    // UNSAFE! 
                    // lock geeft een deadlock hier bij click events. Dus heeft een andere thread de lock nog. 
                    OldUnitContainer.RemoveUnit(this);
                }
            }
            else
            {
                Console.WriteLine("Unit is al verwijderd!");
            }
        }

        public void Move()
        {
            if (MoveBehavior != null)
            {
                MoveBehavior.NewFrame();
            }
        }

        public void Draw()
        {
            if (DrawBehavior != null)
            {
                DrawBehavior.Draw();
            }
        }
    }
}