using DuckHunt.Factories;
using DuckHunt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckHunt.Controllers;

namespace DuckHunt.Behaviors.OldMove
{
    class SimpleMoveBehavior : BaseMoveBehavior
    {
        public SimpleMoveBehavior() : base()
        {
            VX = 500;
            VY = 300;
        }

        public static void RegisterSelf()
        {
            //OldMoveBehaviorFactory.register("simple", typeof(SimpleMoveBehavior));
        }

        public override void Move(IGame game)
        {
            baseMove(game);

            if (!removeIfExpired(game))
            {
                EnsureInScreenX(true);
                EnsureInScreenY(true);
            }
        }
    }
}
