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
        public Bunny() : base(90, 78, -78, 0, 5)
        { }

        public Bunny(double width, double height, double posX, double posY, double maxLifeTime)
            : base(width, height, posX, posY, maxLifeTime)
        { }

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
                new KeyValuePair<string, object[]>("spritesheet", new object[] { "BunnyRun2.png", 3, 1, 39, 45, 0.09 }),
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
                destroy();
            }
        }

        public override void init(double width, double height, double posX, double posY, double maxLifeTime)
        {

        }
    }
}
