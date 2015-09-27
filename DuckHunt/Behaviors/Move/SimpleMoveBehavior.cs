using DuckHunt.Factories;
using DuckHunt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt.Behaviors.Move
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
            MoveBehaviorFactory.register("simple", typeof(SimpleMoveBehavior));
        }

        protected override void Move()
        {
            baseMove();
            if (!ThisUnit.isMaxTimeVisableExpired())
            {
                EnsureInScreenX(true);
            }
            EnsureInScreenY(true);

            ThisUnit.removeWhenDisappeared();
        }
    }
}
