using DuckHunt.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckHunt.Controllers;

namespace DuckHunt.Units.Bunny
{
    class Bunny : Unit
    {
        public Bunny(string name) : base(name, 80,80)
        {
            
        }

        public override void init(IGame game)
        {
            #region Random beginpositie
            // Willekeurig links of rechts, niet veel te randomizen hier. 
            if (game.Random.Next(2) == 0)
                PosX = -Width;
            else
                PosX = CONSTANTS.CANVAS_WIDTH;

            // Hoogte staat vast
            PosY = CONSTANTS.CANVAS_HEIGHT - Height;
            #endregion

            #region Random snelheid
            VX = game.Random.Next(400, 600);
            VY = 0;
            #endregion

            State = UnitFactories.States.Create(Name, "alive");
        }
    }
}
