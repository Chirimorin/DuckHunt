using DuckHunt.Controllers;
using DuckHunt.Factories;
using DuckHunt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DuckHunt.Behaviors.OldMove
{
    public class AfraidOfMouseMoveBehavior : RandomMoveBehavior
    {
        public AfraidOfMouseMoveBehavior() : base() { }

        public new static void RegisterSelf()
        {
            //OldMoveBehaviorFactory.register("afraid", typeof(AfraidOfMouseMoveBehavior));
        }

        protected override void updateGoals(IGame game, bool force = false)
        {
            randomGoals(game, force);

            if (ThisUnit != null)
            {
                if ((PosXMiddle > game.InputContainer.MousePosition.X &&
                GoalVX < 0) ||
                (PosXMiddle < game.InputContainer.MousePosition.X &&
                GoalVX > 0))
                {
                    GoalVX = -GoalVX;
                    DVX = -DVX;
                }

                if ((PosYMiddle > game.InputContainer.MousePosition.Y &&
                GoalVY < 0) ||
                (PosYMiddle < game.InputContainer.MousePosition.Y &&
                GoalVY > 0))
                {
                    GoalVY = -GoalVY;
                    DVY = -DVY;
                }
            }
        }
    }
}
