using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckHunt.Controllers;
using DuckHunt.Units;

namespace DuckHunt.Behaviors.Move.Chicken
{
    public class FleeingChickenMoveBehavior : BaseMoveBehavior
    {
        public FleeingChickenMoveBehavior(double dVX, double dVY, double maxVX, double maxVY) : base(dVX, dVY)
        {
            MaxVX = maxVX;
            MaxVY = maxVY;
        }

        public override void Move(Unit unit, IGame game)
        {
            if ((unit.VX < 0 &&
                DVX > 0) ||
                    (unit.VX > 0 &&
                    DVX < 0))
                DVX = -DVX;

            BaseMove(unit, game);
        }
    }
}
