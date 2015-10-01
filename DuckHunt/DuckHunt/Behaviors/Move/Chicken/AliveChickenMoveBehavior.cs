using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckHunt.Controllers;
using DuckHunt.Units;

namespace DuckHunt.Behaviors.Move.Chicken
{
    public class AliveChickenMoveBehavior : BaseMoveBehavior
    {
        private double _goalVX = double.NaN;
        protected double GoalVX
        {
            get { return _goalVX; }
            set { _goalVX = value; }
        }

        private double _goalVY = double.NaN;
        protected double GoalVY
        {
            get { return _goalVY; }
            set { _goalVY = value; }
        }

        public AliveChickenMoveBehavior(double dVX, double dVY, double maxVX, double maxVY) : base(dVX, dVY)
        {
            MaxVX = maxVX;
            MaxVY = maxVY;
        }

        public override void Move(Unit unit, IGame game)
        {
            BaseMove(unit, game);

            SetGoals(game, unit);

            double minX = CONSTANTS.CANVAS_WIDTH * 0.25;
            double minY = CONSTANTS.CANVAS_HEIGHT * 0.25;
            MoveIntoArea(unit,
                         minX,
                         CONSTANTS.CANVAS_WIDTH - minX,
                         minY,
                         CONSTANTS.CANVAS_HEIGHT - minY);

            if (screenEntered(unit))
            {
                bounceLeft(unit);
                bounceRight(unit);
                bounceTop(unit);
                bounceBottom(unit);
            }
            else
            {
                MoveIntoScreen(unit);
            }
        }

        protected override void FlipXAcceleration(Unit unit)
        {
            base.FlipXAcceleration(unit);
            GoalVX = -GoalVX;
        }

        protected override void FlipYAcceleration(Unit unit)
        {
            base.FlipYAcceleration(unit);
            GoalVY = -GoalVY;
        }

        protected virtual void SetGoals(IGame game, Unit unit)
        {
            if (ReachedXGoal(game, unit))
            {
                GoalVX = GetRandomGoal(game, (int)(MaxVX / 2), (int)(MaxVX));
            }

            if (ReachedYGoal(game, unit))
            {
                GoalVY = GetRandomGoal(game, (int)(MaxVY / 2), (int)(MaxVY));
            }

            AccelerateToGoals();
        }

        protected virtual double GetRandomGoal(IGame game, int minSpeed, int maxSpeed)
        {
            double result = game.Random.Next(minSpeed, maxSpeed);
            return (game.Random.Next(0, 2) == 0 ? result : -result);
        }

        /// <summary>
        /// Controleert of de X-doelsnelheid is bereikt
        /// </summary>
        /// <param name="game">Instantie van game</param>
        /// <param name="unit">De unit</param>
        /// <returns>true als de doelsnelheid is bereikt</returns>
        protected virtual bool ReachedXGoal(IGame game, Unit unit)
        {
            return (double.IsNaN(GoalVX) ||
                (GoalVX < 0 && unit.VX < GoalVX) ||
                (GoalVX > 0 && unit.VX > GoalVX));
        }

        /// <summary>
        /// Controleert of de Y-doelsnelheid is bereikt
        /// </summary>
        /// <param name="game">Instantie van game</param>
        /// <param name="unit">De unit</param>
        /// <returns>true als de doelsnelheid is bereikt</returns>
        protected virtual bool ReachedYGoal(IGame game, Unit unit)
        {
            return (double.IsNaN(GoalVY) || 
                (GoalVY < 0 && unit.VY < GoalVY) ||
                (GoalVY > 0 && unit.VY > GoalVY));
        }

        protected virtual void AccelerateToGoals()
        {
            if ((DVX < 0 && GoalVX > 0) ||
                (DVX > 0 && GoalVX < 0))
                DVX = -DVX;

            if ((DVY < 0 && GoalVY > 0) ||
                (DVY > 0 && GoalVY < 0))
                DVY = -DVY;
        }

        protected virtual void MoveIntoArea(Unit unit, double minX, double maxX, double minY, double maxY)
        {
            if ((unit.PosXMiddle < minX && GoalVX < 0) ||
                (unit.PosXMiddle > maxX && GoalVX > 0))
                FlipXAcceleration(unit);

            if ((unit.PosYMiddle < minY && GoalVY < 0) ||
                (unit.PosYMiddle > maxY && GoalVY > 0))
                FlipYAcceleration(unit);
        }
    }
}
