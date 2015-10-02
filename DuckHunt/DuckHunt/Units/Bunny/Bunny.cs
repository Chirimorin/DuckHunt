using DuckHunt.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt.Units.Bunny
{
    class Bunny : Unit
    {
        public Bunny(string name,
            double width,
            double height,
            double posX,
            double posY,
            double vX,
            double vY)
            : base(name, width, height, posX, posY, vX, vY)
        {
            State = StateFactory.createState(Name, "alive");
        }
    }
}
