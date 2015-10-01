using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DuckHunt.Behaviors.OldMove;
using DuckHunt.Factories;

namespace DuckHunt.Units.Chicken
{
    public class FleeingChickenState : BaseUnitState
    {
        public FleeingChickenState(string unit, string name) : base(unit, name)
        {
            //DrawBehavior = OldDrawBehaviorFactory.Instance.createDrawBehavior(new KeyValuePair<string, object[]>("spritesheet", new object[] { "ChickenFly.png", 4, 2, 97, 72, 0.07 }));
            //MoveBehavior = OldMoveBehaviorFactory.Instance.createMoveBehavior(new KeyValuePair<string, object[]>("random", null));
        }

        public override void onClick(Unit unit, Point point)
        {
            if (unit.isHit(point))
            {
                unit.State = StateFactory.createState(unit.Name, "dead");
            }
        }
    }
}
