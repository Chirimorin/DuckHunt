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
        public Bunny(IGame game) : base(game, 90, 78, -78, 0, 5)
        {
            lock (Locks.ActionContainer)
            {
                PosYBottom = OldActionContainer.Instance.WindowHeight;
            }
        }

        public Bunny(IGame game, double width, double height, double posX, double posY, double maxLifeTime)
            : base(game, width, height, posX, posY, maxLifeTime)
        { }

        private KeyValuePair<string, object[]>[] possibleMoveBehaviors =
            {
                new KeyValuePair<string, object[]>("horizontal", null),
                new KeyValuePair<string, object[]>("gravity", new object[] { 5000.0, 2000.0, 0.5, 1000.0, 500.0 }),
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
                //new KeyValuePair<string, object[]>("simple", null),
                new KeyValuePair<string, object[]>("spritesheet", new object[] { "BunnyRun2.png", 3, 1, 39, 45, 0.09 }),
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
            UnitFactory.register("bunny", typeof(Bunny));
        }

        public override void onClick(Point point)
        {
            if (isHit(point))
            {
                destroy();
            }
        }
    }
}
