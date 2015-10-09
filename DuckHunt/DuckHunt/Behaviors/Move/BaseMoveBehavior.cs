using DuckHunt.Controllers;
using DuckHunt.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt.Behaviors.Move
{
    public abstract class BaseMoveBehavior
    {
        #region versnelling
        private double _dVX;
        /// <summary>
        /// X-versnelling (pixels/seconde/seconde)
        /// </summary>
        public double DVX
        {
            get { return _dVX; }
            set { _dVX = value; }
        }

        private double _dVY;
        /// <summary>
        /// Y-versnelling (pixels/seconde/seconde)
        /// </summary>
        public virtual double DVY
        {
            get { return _dVY; }
            set { _dVY = value; }
        }

        private double _maxVX = double.PositiveInfinity;
        public double MaxVX
        {
            get { return _maxVX; }
            set { _maxVX = value; }
        }

        private double _maxVY = double.PositiveInfinity;
        public double MaxVY
        {
            get { return _maxVY; }
            set { _maxVY = value; }
        }
        #endregion

        private bool _screenEntered = false;
        private bool ScreenEntered
        {
            get { return _screenEntered; }
            set { _screenEntered = value; }
        }


        public BaseMoveBehavior(double dVX, double dVY)
        {
            DVX = dVX;
            DVY = dVY;
        }

        #region Events
        /// <summary>
        /// Wordt een vast aantal keren per seconde aangeroepen. 
        /// Alle random zonder condities (zoals een random kans om te springen) horen hier in te staan. 
        /// </summary>
        public virtual void FixedTimePassed(Unit unit, IGame game) { }

        /// <summary>
        /// De beweeg logica, deze moet uniek zijn voor elke moveBehavior
        /// </summary>
        public abstract void Move(Unit unit, IGame game);
        #endregion

        #region Beweging
        /// <summary>
        /// Basis beweging over de X-as
        /// </summary>
        protected virtual void BaseMoveX(Unit unit, IGame game)
        {
            unit.VX += GetTimeBased(DVX, game);
            if (unit.VX > MaxVX)
                unit.VX = MaxVX;
            else if (unit.VX < -MaxVX)
                unit.VX = -MaxVX;

            unit.PosX += GetTimeBased(unit.VX * game.CurrentLevel.SpeedModifier, game);
        }

        /// <summary>
        /// Basis beweging over de Y-as
        /// </summary>
        protected virtual void BaseMoveY(Unit unit, IGame game)
        {
            unit.VY += GetTimeBased(DVY, game);
            if (unit.VY > MaxVY)
                unit.VY = MaxVY;
            else if (unit.VY < -MaxVY)
                unit.VY = -MaxVY;

            unit.PosY += GetTimeBased(unit.VY * game.CurrentLevel.SpeedModifier, game);
        }

        /// <summary>
        /// Basis beweging over de X en Y-as
        /// </summary>
        protected virtual void BaseMove(Unit unit, IGame game)
        {
            BaseMoveX(unit, game);
            BaseMoveY(unit, game);
        }

        protected virtual void MoveIntoScreen(Unit unit)
        {
            if ((unit.PosX < 0 && unit.VX < 0) ||
                (unit.PosXRight > CONSTANTS.CANVAS_WIDTH && unit.VX > 0))
                FlipXSpeed(unit);
            
            if ((unit.PosY < 0 && unit.VY < 0) ||
                (unit.PosYBottom > CONSTANTS.CANVAS_HEIGHT && unit.VY > 0))
                FlipYSpeed(unit);
        }

        protected virtual void FlipXAcceleration(Unit unit)
        {
            DVX = -DVX;
        }

        protected virtual void FlipXSpeed(Unit unit)
        {
            FlipXAcceleration(unit);
            unit.VX = -unit.VX;
        }

        protected virtual void FlipYAcceleration(Unit unit)
        {
            DVY = -DVY;
        }

        protected virtual void FlipYSpeed(Unit unit)
        {
            FlipYAcceleration(unit);
            unit.VY = -unit.VY;
        }
        #endregion

        #region Collision
        protected bool isInScreenLeft(Unit unit)
        {
            return unit.PosX >= 0;
        }

        protected virtual bool bounceLeft(Unit unit)
        {
            if (!isInScreenLeft(unit))
            {
                unit.PosX = 0;
                FlipXSpeed(unit);
                return true;
            }
            return false;
        }

        protected bool isInScreenRight(Unit unit)
        {
            return unit.PosXRight <= CONSTANTS.CANVAS_WIDTH;
        }

        protected virtual bool bounceRight(Unit unit)
        {
            if (!isInScreenRight(unit))
            {
                unit.PosXRight = CONSTANTS.CANVAS_WIDTH;
                FlipXSpeed(unit);
                return true;
            }
            return false;
        }

        protected bool isInScreenTop(Unit unit)
        {
            return unit.PosY >= 0;
        }

        protected virtual bool bounceTop(Unit unit)
        {
            if (!isInScreenTop(unit))
            {
                unit.PosY = 0;
                FlipYSpeed(unit);
                return true;
            }
            return false;
        }

        protected bool isInScreenBottom(Unit unit)
        {
            return unit.PosYBottom <= CONSTANTS.CANVAS_HEIGHT;
        }

        protected virtual bool bounceBottom(Unit unit)
        {
            if (!isInScreenBottom(unit))
            {
                unit.PosYBottom = CONSTANTS.CANVAS_HEIGHT;
                FlipYSpeed(unit);
                return true;
            }
            return false;
        }

        protected bool screenEntered(Unit unit)
        {
            if (!ScreenEntered)
            {
                ScreenEntered = (isInScreenBottom(unit) &&
                                 isInScreenTop(unit) &&
                                 isInScreenLeft(unit) &&
                                 isInScreenRight(unit));
            }
            return ScreenEntered;
        }
        #endregion

        /// <summary>
        /// Veranderd een waarde in de tijd gebaseerde waarde
        /// </summary>
        /// <param name="value">De waarde per seconde</param>
        /// <returns>De waarde voor deze frame</returns>
        protected double GetTimeBased(double value, IGame game)
        {
            return value * game.DT;
        }
    }
}
