using DuckHunt.Behaviors.Draw;
using DuckHunt.Factories;
using DuckHunt.Behaviors.Move;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows;
using DuckHunt.Controllers;
using DuckHunt.Behaviors;

namespace DuckHunt.Model
{
    public class Chicken : Unit
    {
        public Chicken() : base(95, 70, -95, -70, 10.0) { }

        public Chicken(double width, double height, double posX, double posY, double maxLifeTime)
            : base(width, height, posX, posY, maxLifeTime)
        { }

        private KeyValuePair<string, object[]>[] possibleMoveBehaviors =
            {
                //new KeyValuePair<string, object[]>("simple", null),
                //new KeyValuePair<string, object[]>("hug", null),
                //new KeyValuePair<string, object[]>("afraid", null),
                new KeyValuePair<string, object[]>("random", null),
            };

        public override KeyValuePair<string, object[]> PreferredMoveBehavior
        {
            get
            {
                return possibleMoveBehaviors[OldGame.Instance.Random.Next(0, possibleMoveBehaviors.Length)];
            }
        }

        private KeyValuePair<string, object[]>[] possibleDrawBehaviors =
            {
                //new KeyValuePair<string, object[]>("image", new object[] { "Chicken.png" }),
                new KeyValuePair<string, object[]>("spritesheet", new object[] { "ChickenFly.png", 4, 2, 97, 72, 0.07 }),
            };

        public override KeyValuePair<string, object[]> PreferredDrawBehavior
        {
            get
            {
                return possibleDrawBehaviors[OldGame.Instance.Random.Next(0, possibleDrawBehaviors.Length)];
            }
        }

        public static void RegisterSelf()
        {
            UnitFactory.register("chicken", typeof(Chicken));
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
