using DuckHunt.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt.Units.Chicken
{
    public class Chicken : Unit
    {
        public Chicken(IGame game)
            : base(game, 95, 70, -95, 0, 1, 1)
        { }

        public static void RegisterSelf()
        {
            //UnitFactory.register("chicken", typeof(Chicken));
        }
    }
}
