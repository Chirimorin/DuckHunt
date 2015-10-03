using DuckHunt.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DuckHunt.Units
{
    public abstract class Unit
    {
        #region Properties
        private readonly string _name;
        public string Name
        {
            get { return _name; }
        }

        #region Afmetingen
        private double _width;
        /// <summary>
        /// Breedte van de Unit (in pixels)
        /// </summary>
        public double Width
        {
            get { return _width; }
            set { _width = value; }
        }

        private double _height;
        /// <summary>
        /// Hoogte van de Unit (in pixels)
        /// </summary>
        public double Height
        {
            get { return _height; }
            set { _height = value; }
        }
        #endregion

        #region Positie
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

        #region Snelheid
        private double _vX;
        /// <summary>
        /// X snelheid
        /// </summary>
        public double VX
        {
            get { return _vX; }
            set { _vX = value; }
        }

        private double _vY;
        /// <summary>
        /// Y snelheid
        /// </summary>
        public double VY
        {
            get { return _vY; }
            set { _vY = value; }
        }

        #endregion

        #region Leeftijd
        private double _birthTime;
        public double BirthTime
        {
            get { return _birthTime; }
            set { _birthTime = value; }
        }

        private bool _isDestroyed = false;
        public bool IsDestroyed
        {
            get { return _isDestroyed; }
            protected set { _isDestroyed = value; }
        }
        #endregion

        #region State
        private BaseUnitState _state;
        /// <summary>
        /// De huidige state van de unit
        /// </summary>
        public BaseUnitState State
        {
            get { return _state; }
            set
            {
                if (_state != null)
                {
                    _state.CleanUp();
                }
                _state = value;
            }
        }
        #endregion
        #endregion

        #region CTOR
        public Unit(string name, 
            double width, 
            double height, 
            double posX, 
            double posY, 
            double vX, 
            double vY)
        {
            _name = name;

            Width = width;
            Height = height;
            PosX = posX;
            PosY = posY;
            VX = vX;
            VY = vY;
        }
        #endregion

        #region Functies
        #region Input
        /// <summary>
        /// Wordt aangeroepen bij elke muisklik
        /// </summary>
        /// <param name="point">Het punt waar geklikt is</param>
        public int onClick(Point point)
        {
            return State.onClick(this, point);
        }
        #endregion

        #region Update
        public void Update(IGame game)
        {
            State.Update(this, game);
        }

        public void FixedTimePassed(IGame game)
        {
            State.FixedTimePassed(this, game);
        }
        #endregion

        #region Draw
        public void Draw(IGame game)
        {
            State.Draw(this, game);
        }
        #endregion

        #region Algemeen
        public virtual void destroy()
        {
            IsDestroyed = true;
            State.CleanUp();
        }

        public virtual bool isHit(Point point)
        {
            return (point.X > PosX &&
                point.X < PosXRight &&
                point.Y > PosY &&
                point.Y < PosYBottom);
        }
        #endregion
        #endregion
    }
}
