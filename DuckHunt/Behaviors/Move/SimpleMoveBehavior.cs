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
        public SimpleMoveBehavior()
        {
            VX = 125;
            VY = 250;
        }

        public static void RegisterSelf()
        {
            MoveBehaviorFactory.register("simple", typeof(SimpleMoveBehavior));
        }

        public override void Move()
        {
            double maxX = 0;
            double maxY = 0;

            double timePassed;

            lock (Locks.ActionContainer)
            {
                maxX = ActionContainer.Instance.WindowWidth - Width;
                maxY = ActionContainer.Instance.WindowHeight - Height;
                timePassed = ActionContainer.Instance.DeltaTime;
            }

            if ((PosX > maxX && VX > 0) ||
                (PosX < 0 && VX < 0))
            {
                VX = -VX;
            }

            if (PosX > maxX)
                PosX = maxX;
            if (PosX < 0)
                PosX = 0;

            if ((PosY > maxY && VY > 0) ||
                (PosY < 0 && VY < 0))
            {
                VY = -VY;
            }

            if (PosY > maxY)
                PosY = maxY;
            if (PosY < 0)
                PosY = 0;


            PosX += VX * timePassed;
            PosY += VY * timePassed;
        }
    }
}
