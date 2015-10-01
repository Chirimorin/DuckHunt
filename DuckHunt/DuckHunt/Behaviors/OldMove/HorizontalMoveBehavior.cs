using DuckHunt.Controllers;
using DuckHunt.Factories;
using DuckHunt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt.Behaviors.OldMove
{
    class HorizontalMoveBehavior : BaseMoveBehavior
    {
        public HorizontalMoveBehavior() : base()
        {
            VX = 500;
            VY = 0;
        }

        public static void RegisterSelf()
        {
            //OldMoveBehaviorFactory.register("horizontal", typeof(HorizontalMoveBehavior));
        }

        public override void Move(IGame game)
        {
            PosYBottom = CONSTANTS.CANVAS_HEIGHT; 

            baseMoveX(game);

            if (!removeIfExpired(game))
            {
                EnsureInScreenX(true);
            }
        }
    }
}
