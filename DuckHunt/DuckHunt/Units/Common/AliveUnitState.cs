using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DuckHunt.Controllers;
using DuckHunt.Factories;

namespace DuckHunt.Units.Common
{
    public class AliveUnitState : BaseUnitState
    {
        protected double FleeTime { get; set; }

        public AliveUnitState(string name, double fleeTime) : base(name)
        {
            FleeTime = fleeTime;
        }

        public AliveUnitState(string name) : base(name)
        {
            FleeTime = 10.0;
        }

        public override int onClick(Unit unit, Point point, IGame game)
        {
            if (unit.isHit(point))
            {
                LevelFactory.Instance.CurrentLevel.Kills++;
                unit.setState(UnitFactories.States.Create(unit.Name, "dead"), game);
                return 1;
            }
            return 0;
        }

        public override void Update(Unit unit, IGame game)
        {
            base.Update(unit, game);
            
            if ((game.Time - unit.BirthTime) > FleeTime)
                unit.setState(UnitFactories.States.Create(unit.Name, "fleeing"), game);
        }
    }
}
