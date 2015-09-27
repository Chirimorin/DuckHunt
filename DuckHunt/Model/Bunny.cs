using DuckHunt.Behaviors;
using DuckHunt.Controllers;
using DuckHunt.Factories;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt.Model
{
    public class Bunny : Unit
    {
        private KeyValuePair<string, object[]>[] possibleMoveBehaviors =
            {
                new KeyValuePair<string, object[]>("horizontal", null)
            };

        public override KeyValuePair<string, object[]> PreferredMoveBehavior
        {
            get
            {
                return possibleMoveBehaviors[GameController.Instance.Random.Next(0, possibleMoveBehaviors.Length)];
            }
        }

        private KeyValuePair<string, object[]>[] possibleDrawBehaviors =
            {
                new KeyValuePair<string, object[]>("spritesheet", new object[] { "BunnyRun.png", 3, 1, 64, 64, 0.09 }),
            };

        public override KeyValuePair<string, object[]> PreferredDrawBehavior
        {
            get
            {
                return possibleDrawBehaviors[GameController.Instance.Random.Next(0, possibleDrawBehaviors.Length)];
            }
        }

        public static void RegisterSelf()
        {
            UnitFactory.register("bunny", typeof(Bunny));
        }

        public override void onClick(Point point)
        {
            if (isHit(point))
            {
                UnitContainer.RemoveUnit(this);
            }
        }

        public override void init(double width, double height, double posX = 0, double posY = 0, int maxTimeVisable = 0)
        {
            Width = 100;
            Height = 100;
            PosX = posX;
            PosY = posY;
            MaxTimeVisable = 5000;
        }
    }
}
