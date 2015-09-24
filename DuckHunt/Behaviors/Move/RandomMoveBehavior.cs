using DuckHunt.Controllers;
using DuckHunt.Factories;
using DuckHunt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt.Behaviors.Move
{
    class RandomMoveBehavior : BaseMoveBehavior
    {
        private double VXMax { get; set; }
        private double VYMax { get; set; }

        private double VXGoal { get; set; }
        private double VYGoal { get; set; }

        private double MaxDVX { get; set; }
        private double MaxDVY { get; set; }

        public RandomMoveBehavior()
        {
            VX = 125;
            VY = 250;

            VXMax = 500;
            VYMax = 500;

            MaxDVX = 500;
            MaxDVY = 500;

            updateGoals(true);
        }

        public static void RegisterSelf()
        {
            MoveBehaviorFactory.register("random", typeof(RandomMoveBehavior));
        }

        public override void Move()
        {
            updateGoals();

            double maxX = 0;
            double maxY = 0;

            double timePassed;

            lock (Locks.ActionContainer)
            {
                maxX = ActionContainer.Instance.WindowWidth - Width;
                maxY = ActionContainer.Instance.WindowHeight - Height;
                timePassed = ActionContainer.Instance.DeltaTime;
            }

            double maxDVX = MaxDVX * timePassed;
            double maxDVY = MaxDVY * timePassed;

            if (Math.Abs(VXGoal - VX) < maxDVX)
                VX = VXGoal;
            else if (VX > VXGoal)
                VX -= maxDVX;
            else if (VX < VXGoal)
                VX += maxDVX;

            if (Math.Abs(VYGoal - VY) < maxDVY)
                VY = VYGoal;
            else if (VY > VYGoal)
                VY -= maxDVY;
            else if (VY < VYGoal)
                VY += maxDVY;

            if ((PosX > maxX && VX > 0) ||
                (PosX < 0 && VX < 0))
            {
                VX = -VX;
                VXGoal = -VXGoal;
            }

            if (PosX > maxX)
                PosX = maxX;
            if (PosX < 0)
                PosX = 0;

            if ((PosY > maxY && VY > 0) ||
                (PosY < 0 && VY < 0))
            {
                VY = -VY;
                VYGoal = -VYGoal;
            }

            if (PosY > maxY)
                PosY = maxY;
            if (PosY < 0)
                PosY = 0;


            PosX += VX * timePassed;
            PosY += VY * timePassed;
        }

        private void updateGoals(bool force = false)
        {
            Random random = GameController.Instance.Random;

            if (force || VX == VXGoal)
            {
                double newGoal = random.Next((int)(VXMax / 2), (int)(VXMax));
                if (random.Next(0, 2) == 0)
                    newGoal = -newGoal;
                VXGoal = newGoal;
            }

            if (force || VY == VYGoal)
            {
                double newGoal = random.Next((int)(VYMax / 2), (int)(VYMax));
                if (random.Next(0, 2) == 0)
                    newGoal = -newGoal;
                VYGoal = newGoal;
            }
        }
    }
}
