using DuckHunt.Controllers;
using DuckHunt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DuckHunt.Behaviors.Move
{
    public abstract class BaseMoveBehavior
    {
        private Unit _thisUnit;
        /// <summary>
        /// De Unit die deze Move behavior heeft. 
        /// De Unit klasse zet deze zelf. 
        /// </summary>
        public virtual Unit ThisUnit {
            get { return _thisUnit; }
            set { _thisUnit = value; }
        }

        protected Random Random { get; set; }

        #region Posities
        /// <summary>
        /// X-positie van de linker rand van de Unit (in pixels)
        /// </summary>
        public double PosX
        {
            get { return ThisUnit.PosX; }
            set { ThisUnit.PosX = value; }
        }
        /// <summary>
        /// X-positie van het midden van de Unit (in pixels)
        /// </summary>
        public double PosXMiddle
        {
            get { return ThisUnit.PosXMiddle; }
            set { ThisUnit.PosXMiddle = value; }
        }
        /// <summary>
        /// X-positie van de rechter rand van de Unit (in pixels)
        /// </summary>
        public double PosXRight
        {
            get { return ThisUnit.PosXRight; }
            set { ThisUnit.PosXRight = value; }
        }

        /// <summary>
        /// Y-positie van de bovenste rand van de Unit (in pixels)
        /// </summary>
        public double PosY
        {
            get { return ThisUnit.PosY; }
            set { ThisUnit.PosY = value; }
        }
        /// <summary>
        /// Y-positie van het midden van de Unit (in pixels)
        /// </summary>
        public double PosYMiddle
        {
            get { return ThisUnit.PosYMiddle; }
            set { ThisUnit.PosYMiddle = value; }
        }
        /// <summary>
        /// Y-positie van de onderste rand van de Unit (in pixels)
        /// </summary>
        public double PosYBottom
        {
            get { return ThisUnit.PosYBottom; }
            set { ThisUnit.PosYBottom = value; }
        }
        #endregion

        #region Afmetingen
        /// <summary>
        /// Breedte van de Unit (in pixels)
        /// </summary>
        public double Width
        {
            get { return ThisUnit.Width; }
            set { ThisUnit.Width = value; }
        }

        /// <summary>
        /// Hoogte van de Unit (in pixels)
        /// </summary>
        public double Height
        {
            get { return ThisUnit.Height; }
            set { ThisUnit.Height = value; }
        }
        #endregion

        #region Snelheid & versnelling
        private double _vX;
        /// <summary>
        /// X-snelheid (pixels/seconde)
        /// Naar links = negatief
        /// Naar rechts = positief
        /// </summary>
        public virtual double VX
        {
            get { return _vX; }
            set
            {
                if (value > MaxVX)
                    _vX = MaxVX;
                else if (value < -MaxVX)
                    _vX = -MaxVX;
                else
                    _vX = value;
            }
        }

        private double _maxVX = double.PositiveInfinity;
        /// <summary>
        /// Maximale X-snelheid (pixels/seconde)
        /// Geldt voor beide richtingen.
        /// </summary>
        public virtual double MaxVX
        {
            get { return _maxVX; }
            set { _maxVX = value; }
        }


        private double _dVX;
        /// <summary>
        /// X-versnelling (pixels/seconde/seconde)
        /// </summary>
        public double DVX
        {
            get { return _dVX; }
            set
            {
                if (value > MaxDVX)
                    _dVX = MaxDVX;
                else if (value < -MaxDVX)
                    _dVX = -MaxDVX;
                else
                    _dVX = value;
            }
        }

        private double _maxDVX = double.PositiveInfinity;
        /// <summary>
        /// Maximale X-versnelling (pixels/seconde/seconde)
        /// </summary>
        public virtual double MaxDVX
        {
            get { return _maxDVX; }
            set { _maxDVX = value; }
        }


        private double _vY;
        /// <summary>
        /// Y-snelheid (pixels/seconde)
        /// Naar boven = negatief
        /// Naar beneden = positief
        /// </summary>
        public virtual double VY
        {
            get { return _vY; }
            set
            {
                if (value > MaxVY)
                    _vY = MaxVY;
                else if (value < -MaxVY)
                    _vY = -MaxVY;
                else
                    _vY = value;
            }
        }

        private double _maxVY = double.PositiveInfinity;
        /// <summary>
        /// Maximale Y-snelheid (pixels/second)
        /// Geldt voor beide richtingen
        /// </summary>
        public virtual double MaxVY
        {
            get { return _maxVY; }
            set { _maxVY = value; }
        }


        private double _dVY;
        /// <summary>
        /// Y-versnelling (pixels/seconde/seconde)
        /// </summary>
        public virtual double DVY
        {
            get { return _dVY; }
            set
            {
                if (value > MaxDVY)
                    _dVY = MaxDVY;
                else if (value < -MaxDVY)
                    _dVY = -MaxDVY;
                else
                    _dVY = value;
            }
        }

        private double _maxDVY = double.PositiveInfinity;
        /// <summary>
        /// Maximale Y-vernselling (pixels/seconde/seconde)
        /// </summary>
        public virtual double MaxDVY
        {
            get { return _maxDVY; }
            set { _maxDVY = value; }
        }
        #endregion

        #region Container data
        private double _windowWidth;
        /// <summary>
        /// Schermbreedte (in pixels)
        /// </summary>
        public double WindowWidth
        {
            get { return _windowWidth; }
            set { _windowWidth = value; }
        }

        private double _windowHeight;
        /// <summary>
        /// Schermhoogte (in pixels)
        /// </summary>
        public double WindowHeight
        {
            get { return _windowHeight; }
            set { _windowHeight = value; }
        }

        private double _dT;
        /// <summary>
        /// De tijd gepasseerd sinds de vorige frame (in seconden)
        /// </summary>
        public double DT
        {
            get { return _dT; }
            set { _dT = value; }
        }

        private Point _mousePosition;
        /// <summary>
        /// De positie van de muis in deze frame.
        /// </summary>
        public Point MousePosition
        {
            get { return _mousePosition; }
            set { _mousePosition = value; }
        }


        #endregion

        public BaseMoveBehavior()
        {
            Random = OldGame.Instance.Random;
        }

        /// <summary>
        /// Reset de data relevant aan een enkele frame en voert Move() uit.
        /// </summary>
        public virtual void NewFrame()
        {
            lock (Locks.ActionContainer)
            {
                WindowWidth = OldActionContainer.Instance.WindowWidth;
                WindowHeight = OldActionContainer.Instance.WindowHeight;
                DT = OldActionContainer.Instance.DeltaTime;
            }

            lock (Locks.InputContainer)
            {
                MousePosition = OldInputContainer.Instance.MousePosition;
            }

            Move();
        }

        /// <summary>
        /// Wordt een vast aantal keren per seconde aangeroepen. 
        /// Alle random zonder condities (zoals een random kans om te springen) horen hier in te staan. 
        /// </summary>
        public virtual void FixedTimePassed() { }

        /// <summary>
        /// De beweeg logica, deze moet uniek zijn voor elke moveBehavior
        /// </summary>
        protected abstract void Move();

        /// <summary>
        /// Zorgt dat de unit horizontaal binnen het scherm is
        /// </summary>
        /// <returns>true als de unit horizontaal buiten het scherm was</returns>
        protected bool EnsureInScreenX(bool flipDirection, bool onlyWhenMovingOut = true)
        {
            bool result = EnsureInScreenX(onlyWhenMovingOut);

            if (flipDirection && result)
                VX = -VX;

            return result;
        }
        private bool EnsureInScreenX(bool onlyWhenMovingOut)
        {
            if (PosX < 0 &&
                (!onlyWhenMovingOut || VX < 0))
            {
                PosX = 0;
                return true;
            }

            if (PosXRight > WindowWidth &&
                (!onlyWhenMovingOut || VX > 0))
            {
                PosXRight = WindowWidth;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Zorgt dat de unit verticaal binnen her scherm is
        /// </summary>
        /// <returns>true als de unit verticaal buiten het scherm was</returns>
        protected bool EnsureInScreenY(bool flipDirection, bool onlyWhenMovingOut = true)
        {
            bool result = EnsureInScreenY(onlyWhenMovingOut);

            if (flipDirection && result)
                VY = -VY;

            return result;
        }
        private bool EnsureInScreenY(bool onlyWhenMovingOut)
        {
            if (PosY < 0 &&
                (!onlyWhenMovingOut || VY < 0))
            {
                PosY = 0;
                return true;
            }

            if (PosYBottom > WindowHeight &&
                (!onlyWhenMovingOut || VY > 0))
            {
                PosYBottom = WindowHeight;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Basis beweging over de X-as
        /// </summary>
        protected virtual void baseMoveX()
        {
            // VX houd zichzelf onder maxVX
            VX += getTimeBased(DVX);
            PosX += getTimeBased(VX);
        }

        /// <summary>
        /// Basis beweging over de Y-as
        /// </summary>
        protected virtual void baseMoveY()
        {
            // VY houd zichzelf onder MaxVY
            VY += getTimeBased(DVY);
            PosY += getTimeBased(VY);
        }

        /// <summary>
        /// Basis beweging over de X en Y-as
        /// </summary>
        protected virtual void baseMove()
        {
            baseMoveX();
            baseMoveY();
        }

        /// <summary>
        /// Als de MaxLifeTime van de unit voorbij is en of de unit buiten het scherm is
        /// </summary>
        /// <returns>true als de MaxLifeTime voorbij is</returns>
        protected virtual bool removeIfExpired()
        {
            if (ThisUnit.isMaxLifetimeExpired())
            {
                if (PosX > WindowWidth ||
                    PosXRight < 0 ||
                    PosY > WindowHeight ||
                    PosYBottom < 0)
                {
                    ThisUnit.destroy();
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Veranderd een waarde in de tijd gebaseerde waarde
        /// </summary>
        /// <param name="value">De waarde per seconde</param>
        /// <returns>De waarde voor deze frame</returns>
        protected double getTimeBased(double value)
        {
            return value * DT;
        }
    }
}
