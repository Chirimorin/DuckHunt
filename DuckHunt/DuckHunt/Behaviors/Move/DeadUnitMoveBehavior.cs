using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckHunt.Controllers;
using DuckHunt.Units;

namespace DuckHunt.Behaviors.Move
{
    public class DeadUnitMoveBehavior : BaseMoveBehavior
    {
        private bool OnGround { get; set; }

        public DeadUnitMoveBehavior(double dVY, double maxVY) : base(0,dVY)
        {
            MaxVY = maxVY;

            OnGround = false;
        }

        public override void Move(Unit unit, IGame game)
        {
            BaseMove(unit, game);

            if (screenEntered(unit) && !isInScreenBottom(unit))
            {
                unit.PosYBottom = CONSTANTS.CANVAS_HEIGHT;

                OnGround = true;
            }
        }

        public override void FixedTimePassed(Unit unit, IGame game)
        {
            if (OnGround)
            {
                unit.VX = 0.90 * unit.VX;
            }
        }

        protected override void bounceBottom(Unit unit)
        {
            base.bounceBottom(unit);
        }
    }
}
