using DuckHunt.Factories;
using DuckHunt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt.Behaviors.Move
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
            MoveBehaviorFactory.register("horizontal", typeof(HorizontalMoveBehavior));
        }

        protected override void Move()
        {
            PosYBottom = WindowHeight; // Ergens anders gaat dit mis, vooral als de window size veranderd.

            baseMoveX();

            if (!ThisUnit.isMaxLifetimeExpired())
            {
                EnsureInScreenX(true);
            }
            else
            {
                // Verwijder de unit als deze uit het beeld is
                if (PosX > WindowWidth ||
                    PosXRight < 0)
                    ThisUnit.destroy();
            }
        }
    }
}
