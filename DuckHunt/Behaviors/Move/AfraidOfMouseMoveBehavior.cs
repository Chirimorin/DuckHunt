using DuckHunt.Controllers;
using DuckHunt.Factories;
using DuckHunt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DuckHunt.Behaviors.Move
{
    public class AfraidOfMouseMoveBehavior : RandomMoveBehavior
    {
        public AfraidOfMouseMoveBehavior() : base() { }

        public new static void RegisterSelf()
        {
            MoveBehaviorFactory.register("afraid", typeof(AfraidOfMouseMoveBehavior));
        }

        protected override void Move()
        {
            base.Move();
        }

        protected override void updateGoals(bool force = false)
        {
            randomGoals(force);

            lock (Locks.InputContainer)
            {
                MousePosition = InputContainer.Instance.MousePosition;
            }

            if (ThisUnit != null)
            {
                if ((PosXMiddle > MousePosition.X &&
                GoalVX < 0) ||
                (PosXMiddle < MousePosition.X &&
                GoalVX > 0))
                {
                    GoalVX = -GoalVX;
                    DVX = -DVX;
                }

                if ((PosYMiddle > MousePosition.Y &&
                GoalVY < 0) ||
                (PosYMiddle < MousePosition.Y &&
                GoalVY > 0))
                {
                    GoalVY = -GoalVY;
                    DVY = -DVY;
                }
            }
        }
    }
}
