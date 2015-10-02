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
    public class FleeingUnitState : BaseUnitState
    {
        public FleeingUnitState(string unit, string name) : base(unit, name)
        {
            
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

            // Als de unit buiten het scherm is, verwijder hem. 
            if (unit.PosXRight < 0 ||
                unit.PosX > CONSTANTS.CANVAS_WIDTH ||
                unit.PosYBottom < 0 ||
                unit.PosY > CONSTANTS.CANVAS_HEIGHT)
                unit.destroy();
        }
    }
}
