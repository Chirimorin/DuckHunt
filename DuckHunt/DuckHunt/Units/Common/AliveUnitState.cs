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

        public AliveUnitState(string unit, string name, double fleeTime) : base(unit, name)
        {
            FleeTime = fleeTime;
        }

        public AliveUnitState(string unit, string name) : base(unit, name)
        {
            FleeTime = 10.0;
        }

        public override int onClick(Unit unit, Point point)
        {
            if (unit.isHit(point))
            {
                LevelFactory.Instance.CurrentLevel.Kills++;
                unit.State = Factory<BaseUnitState>.Create(unit.Name + "dead"); //StateFactory.createState(unit.Name, "dead");
                return 1;
            }
            return 0;
        }

        public override void Update(Unit unit, IGame game)
        {
            base.Update(unit, game);
            
            if ((game.Time - unit.BirthTime) > FleeTime)
                unit.State = Factory<BaseUnitState>.Create(unit.Name + "fleeing"); //StateFactory.createState(unit.Name, "fleeing");
        }
    }
}
