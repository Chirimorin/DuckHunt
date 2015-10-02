using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckHunt.Controllers;
using DuckHunt.Units;

namespace DuckHunt.Behaviors.Move.Running
{
    class RunningFleeMoveBehavior : GravityMoveBehavior
    {
        public RunningFleeMoveBehavior(double dVX, double dVY, double maxVX, double maxVY, double jumpPower, double bouncyness, int jumpChance) : base(dVX, dVY, maxVX, maxVY, jumpPower, bouncyness, jumpChance)
        {
        }

        public override void Move(Unit unit, IGame game)
        {
            BaseMove(unit, game);

            if (bounceBottom(unit))
            {
                unit.VY = unit.VY * Bouncyness;

                if (unit.VY >= GetTimeBased(-DVY, game))
                {
                    unit.VY = 0;
                }
            }
        }
    }
}
