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
    public class GravityMoveBehavior : BaseMoveBehavior
    {
        private double VXMax { get; set; }
        private double VYMax { get; set; }

        private double VXGoal { get; set; }
        private double VYGoal { get; set; }

        private double MaxDVX { get; set; }
        private double MaxDVY { get; set; }

        public GravityMoveBehavior()
        {
            VX = 125;
            VY = 250;

            VXMax = 500;
            VYMax = 2000;

            MaxDVX = 500;
            MaxDVY = 1500;

            VYGoal = 2500;

            updateGoals(true);
        }

        public static void RegisterSelf()
        {
            MoveBehaviorFactory.register("gravity", typeof(GravityMoveBehavior));
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
                VY = -VY * 0.5;
                if (VY > -75)
                    VY = 0;
            }

            if (PosY > maxY)
            {
                PosY = maxY;

                if (GameController.Instance.Random.Next(0, 10) == 0)
                {
                    VY = -1000;
                }
            }
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

            if (Parent != null)
            {
                double windowWidth;
                double windowHeight;

                lock (Locks.ActionContainer)
                {
                    windowWidth = ActionContainer.Instance.WindowWidth;
                    windowHeight = ActionContainer.Instance.WindowHeight;
                }

                double minX = windowWidth / 4;
                double maxX = windowWidth - minX;
                double maxY = windowHeight - Height;

                if ((PosX + (0.5 * Width)) < minX &&
                    VXGoal < 0)
                    VXGoal = -VXGoal;
                else if ((PosX + (0.5 * Width)) > maxX &&
                    VXGoal > 0)
                    VXGoal = -VXGoal;
            }
        }
    }
}
