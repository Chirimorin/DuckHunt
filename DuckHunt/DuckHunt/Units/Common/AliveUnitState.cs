using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DuckHunt.Behaviors.OldMove;
using DuckHunt.Controllers;
using DuckHunt.Factories;

namespace DuckHunt.Units.Common
{
    public class AliveUnitState : BaseUnitState
    {
        private double Timer { get; set; }
        private double FleeTime { get; set; }

        public AliveUnitState(string unit, string name, double fleeTime) : base(unit, name)
        {
            Timer = 0;
            FleeTime = fleeTime;
        }

        public override void onClick(Unit unit, Point point)
        {
            if (unit.isHit(point))
            {
                unit.State = StateFactory.createState(unit.Name, "dead");
            }
        }

        public override void Update(Unit unit, IGame game)
        {
            base.Update(unit, game);

            Timer += game.DT;

            if (Timer > FleeTime)
                unit.State = StateFactory.createState(unit.Name, "fleeing");
        }
    }
}
