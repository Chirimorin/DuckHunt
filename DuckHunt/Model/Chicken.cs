using DuckHunt.Behaviors.Draw;
using DuckHunt.Factories;
using DuckHunt.Behaviors.Move;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace DuckHunt.Model
{
    public class Chicken : Unit
    {
        public static void RegisterSelf()
        {
            UnitFactory.register("chicken", typeof(Chicken));
        }
    }
}
