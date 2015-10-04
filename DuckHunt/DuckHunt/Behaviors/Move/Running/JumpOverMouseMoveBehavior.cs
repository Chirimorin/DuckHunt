using DuckHunt.Controllers;
using DuckHunt.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DuckHunt.Behaviors.Move.Running
{
    public class JumpOverMouseMoveBehavior : GravityMoveBehavior
    {
        public JumpOverMouseMoveBehavior(double dVX, double dVY, double maxVX, double maxVY, double jumpPower, double bouncyness) : base(dVX, dVY, maxVX, maxVY, jumpPower, bouncyness, 0)
        {
        }

        public override void Move(Unit unit, IGame game)
        {
            base.Move(unit, game);

            // Unit moet op de grond lopen voor een sprong
            if (unit.PosYBottom == CONSTANTS.CANVAS_HEIGHT &&
                unit.VY == 0)
            {
                // Tijd die het duurt om aan de top van de sprong te komen
                double timeUntilTop = JumpPower / DVY;

                // Hoogte van de sprong
                double jumpHeight = (JumpPower * 0.5) * timeUntilTop; 

                // X-afstand afgelegt in deze tijd
                double expectedPosX = unit.PosXMiddle + (timeUntilTop * unit.VX); // Uitgaande van een constante VX

                Point mousePosition = game.InputContainer.MousePosition;

                if ((CONSTANTS.CANVAS_HEIGHT - mousePosition.Y) < jumpHeight && // Halen we de sprong over de muis?
                    Math.Abs(mousePosition.X - expectedPosX) < 25) // Zitten we ongeveer boven de muis aan de top?
                {
                    Jump(unit);
                }

            }
        }

        public override void FixedTimePassed(Unit unit, IGame game)
        {
            // Override om de random jump te voorkomen.
        }
    }
}
