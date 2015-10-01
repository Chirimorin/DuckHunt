using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckHunt.Controllers;
using DuckHunt.Units;

namespace DuckHunt.Behaviors.Move
{
    public class SimpleMoveBehavior : BaseMoveBehavior
    {
        public SimpleMoveBehavior(double dVX, double dVY) : base(dVX, dVY)
        {
            
        }

        public override void Move(Unit unit, IGame game)
        {
            BaseMove(unit, game);

            if (screenEntered(unit))
            {
                Console.WriteLine("Unit is in screen: {0};{1}", unit.PosX, unit.PosY);
                bounceLeft(unit);
                bounceRight(unit);
                bounceTop(unit);
                bounceBottom(unit);
            }
            else
            {
                Console.WriteLine("Unit is not in screen");
                MoveIntoScreen(unit);
            }
        }
    }
}
