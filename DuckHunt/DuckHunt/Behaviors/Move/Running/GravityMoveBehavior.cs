using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckHunt.Controllers;
using DuckHunt.Units;

namespace DuckHunt.Behaviors.Move.Running
{
    class GravityMoveBehavior : BaseMoveBehavior
    {
        protected double Bouncyness { get; set; }
        protected double JumpPower { get; set; }
        protected int JumpChance { get; set; }

        public GravityMoveBehavior(double dVX, double dVY, double maxVX, double maxVY, double jumpPower, double bouncyness, int jumpChance) : base(dVX, dVY)
        {
            MaxVX = maxVX;
            MaxVY = maxVY;

            JumpPower = jumpPower;
            Bouncyness = bouncyness;
            JumpChance = jumpChance;
        }

        public override void Move(Unit unit, IGame game)
        {
            BaseMove(unit, game);

            if (bounceBottom(unit))
            {
                if (unit.VY >= GetTimeBased(DVY, game))
                {
                    unit.VY = 0;
                }
                else
                {
                    unit.VY = unit.VY * Bouncyness;
                }
            }

            if (screenEntered(unit))
            {
                bounceLeft(unit);
                bounceRight(unit);
                bounceTop(unit);
            }
            else
            {
                MoveIntoScreen(unit);
            }
        }

        public override void FixedTimePassed(Unit unit, IGame game)
        {
            if (unit.PosYBottom == CONSTANTS.CANVAS_HEIGHT &&
                unit.VY == 0 &&
                game.Random.Next(0, JumpChance) == 0)
                Jump(unit);
        }

        /// <summary>
        /// Laat de unit springen
        /// </summary>
        protected void Jump(Unit unit)
        {
            unit.VY = -JumpPower;
        }

        protected override void FlipYAcceleration(Unit unit)
        {
            // Y-versnelling moet gelijk blijven
        }
    }
}
