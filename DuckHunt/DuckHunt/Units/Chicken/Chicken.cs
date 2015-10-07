using DuckHunt.Controllers;
using DuckHunt.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt.Units.Chicken
{
    public class Chicken : Unit
    {
        public Chicken(string name, 
            double width,
            double height,
            double posX,
            double posY,
            double vX,
            double vY)
            : base(name, width, height, posX, posY, vX, vY)
        {
            
        }

        public Chicken(string name) : base (name, 95, 70, -95, 0, 500, 500)
        {
            State = Factory<BaseUnitState>.Create("chickenalive");
        }
        
    }
}
