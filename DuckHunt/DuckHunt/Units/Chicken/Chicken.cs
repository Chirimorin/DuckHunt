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
        public Chicken(string name) : base (name, 95, 70)
        {
            
        }

        public override void init(IGame game)
        {
            #region Random beginpositie
            // Kies eerst een beginplek: links, boven of rechts van het scherm
            int location = game.Random.Next(3);

            if (location == 0 || // Links van het scherm
                location == 2) // Rechts van het scherm
            {
                if (location == 0)
                    PosX = -Width;
                else
                    PosX = CONSTANTS.CANVAS_WIDTH;

                PosY = game.Random.Next(CONSTANTS.CANVAS_HEIGHT) - Height;
            }
            else // Boven het scherm
            {
                PosX = game.Random.Next((int)-Width, CONSTANTS.CANVAS_WIDTH);
                PosY = -Height;
            }
            #endregion

            #region Random snelheid
            VX = game.Random.Next(750, 1000);
            VY = game.Random.Next(750, 1000);
            #endregion

            State = UnitFactories.States.Create(Name, "alive");
        }
    }
}
