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
    public class RandomMoveBehavior : BaseMoveBehavior
    {
        protected double GoalVX { get; set; }
        protected double GoalVY { get; set; }

        public RandomMoveBehavior() : base()
        {
            VX = 125;
            VY = 250;

            MaxVX = 1000;
            MaxVY = 1000;

            DVX = 1000;
            DVY = 1000;

            updateGoals(true);
        }

        public static void RegisterSelf()
        {
            MoveBehaviorFactory.register("random", typeof(RandomMoveBehavior));
        }

        protected override void Move()
        {
            baseMove();

            updateGoals();

            if (!removeIfExpired())
            {
                if (EnsureInScreenX(false))
                {
                    DVX = -DVX;
                    VX = -VX;
                    GoalVX = -GoalVX;
                }

                if (EnsureInScreenY(false))
                {
                    DVY = -DVY;
                    VY = -VY;
                    GoalVY = -GoalVY;
                }
            }
        }

        protected virtual void updateGoals(bool force = false)
        {
            randomGoals(force);

            // Ga richting het midden van het scherm als minder dan 25% van de rand af.
            if (ThisUnit != null)
            {
                double minX = WindowWidth / 4;
                double middleX = minX * 2;
                double maxX = WindowWidth - minX;
                double minY = WindowHeight / 4;
                double maxY = WindowHeight - minY;



                // Als de lifetime van de unit voorbij is, ga dan juist wel uit het scherm. 
                if (ThisUnit.isMaxLifetimeExpired())
                {
                    if ((PosXMiddle < middleX &&
                        GoalVX > 0) ||
                        (PosXMiddle > middleX &&
                        GoalVX < 0))
                    GoalVX = -GoalVX;
                    DVX = -DVX;
                }
                else
                {
                    if ((PosXMiddle < minX &&
                        GoalVX < 0) ||
                        (PosXMiddle > maxX &&
                        GoalVX > 0))
                    {
                        GoalVX = -GoalVX;
                        DVX = -DVX;
                    }
                }

                if ((PosYMiddle < minY &&
                    GoalVY < 0) ||
                    (PosYMiddle > maxY &&
                    GoalVY > 0))
                {
                    GoalVY = -GoalVY;
                    DVY = -DVY;
                }
            }
        }

        protected virtual void randomGoals(bool force = false)
        {
            // Update doelsnelheid voor X
            if (force ||
                GoalVX < 0 && VX < GoalVX ||
                GoalVX > 0 && VX > GoalVX)
            {
                // Kies een nieuwe doelsnelheid
                double newGoal = Random.Next((int)(MaxVX / 2), (int)(MaxVX));
                // 50% kans op links of rechts
                if (Random.Next(0, 2) == 0)
                    newGoal = -newGoal;
                GoalVX = newGoal;
            }

            // Update doelsnelheid voor Y
            if (force ||
                GoalVY < 0 && VY < GoalVY ||
                GoalVY > 0 && VY > GoalVY)
            {
                // Kies een nieuwe doelsnelheid
                double newGoal = Random.Next((int)(MaxVY / 2), (int)(MaxVY));
                // 50% kans op links of rechts
                if (Random.Next(0, 2) == 0)
                    newGoal = -newGoal;
                GoalVY = newGoal;
            }

            // X versnelling moet richting doelsnelheid gaan
            if (DVX < 0 && GoalVX > 0 ||
                DVX > 0 && GoalVX < 0)
                DVX = -DVX;

            // Y versnelling moet richting doelsnelheid gaan
            if (DVY < 0 && GoalVY > 0 ||
                DVY > 0 && GoalVY < 0)
                DVY = -DVY;
        }
    }
}
