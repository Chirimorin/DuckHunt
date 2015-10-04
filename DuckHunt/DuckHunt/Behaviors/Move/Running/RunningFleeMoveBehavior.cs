using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckHunt.Controllers;
using DuckHunt.Units;

namespace DuckHunt.Behaviors.Move.Running
{
    class RunningFleeMoveBehavior : JumpOverMouseMoveBehavior
    {
        public RunningFleeMoveBehavior(double dVX, double dVY, double maxVX, double maxVY, double jumpPower, double bouncyness) : base(dVX, dVY, maxVX, maxVY, jumpPower, bouncyness)
        {
        }

        public override void Move(Unit unit, IGame game)
        {
            base.Move(unit, game);
        }

        protected override bool bounceTop(Unit unit)
        {
            return false;
        }
        protected override bool bounceLeft(Unit unit)
        {
            return false;
        }
        protected override bool bounceRight(Unit unit)
        {
            return false;
        }
        protected override void MoveIntoScreen(Unit unit)
        {
            // Doe niks (unit probeert juist uit het scherm te komen)
        }
    }
}
