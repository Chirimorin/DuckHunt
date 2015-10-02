﻿using DuckHunt.Controllers;
using DuckHunt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DuckHunt.Behaviors.OldMove
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

        /// <summary>
        /// Wordt een vast aantal keren per seconde aangeroepen. 
        /// Alle random zonder condities (zoals een random kans om te springen) horen hier in te staan. 
        /// </summary>
        public virtual void FixedTimePassed(IGame game) { }

        /// <summary>
        /// De beweeg logica, deze moet uniek zijn voor elke moveBehavior
        /// </summary>
        public abstract void Move(IGame game);

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

            if (PosXRight > CONSTANTS.CANVAS_WIDTH &&
                (!onlyWhenMovingOut || VX > 0))
            {
                PosXRight = CONSTANTS.CANVAS_WIDTH;
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

            if (PosYBottom > CONSTANTS.CANVAS_HEIGHT &&
                (!onlyWhenMovingOut || VY > 0))
            {
                PosYBottom = CONSTANTS.CANVAS_HEIGHT;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Basis beweging over de X-as
        /// </summary>
        protected virtual void baseMoveX(IGame game)
        {
            // VX houd zichzelf onder maxVX
            VX += getTimeBased(DVX, game);
            PosX += getTimeBased(VX, game);
        }

        /// <summary>
        /// Basis beweging over de Y-as
        /// </summary>
        protected virtual void baseMoveY(IGame game)
        {
            // VY houd zichzelf onder MaxVY
            VY += getTimeBased(DVY, game);
            PosY += getTimeBased(VY, game);
        }

        /// <summary>
        /// Basis beweging over de X en Y-as
        /// </summary>
        protected virtual void baseMove(IGame game)
        {
            baseMoveX(game);
            baseMoveY(game);
        }

        /// <summary>
        /// Als de MaxLifeTime van de unit voorbij is en of de unit buiten het scherm is
        /// </summary>
        /// <returns>true als de MaxLifeTime voorbij is</returns>
        protected virtual bool removeIfExpired(IGame game)
        {
            if (ThisUnit.isMaxLifetimeExpired(game))
            {
                if (PosX > CONSTANTS.CANVAS_WIDTH ||
                    PosXRight < 0 ||
                    PosY > CONSTANTS.CANVAS_HEIGHT ||
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
        protected double getTimeBased(double value, IGame game)
        {
            return value * game.DT;
        }
    }
}