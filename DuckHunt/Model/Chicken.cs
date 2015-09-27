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
        private KeyValuePair<string, object[]>[] possibleMoveBehaviors =
            {
                new KeyValuePair<string, object[]>("simple", null),
                /*new KeyValuePair<string, object[]>("hug", null),
                new KeyValuePair<string, object[]>("afraid", null),
                new KeyValuePair<string, object[]>("random", null),
                new KeyValuePair<string, object[]>("gravity", null),*/
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
                //new KeyValuePair<string, object[]>("image", new object[] { "Chicken.png" }),
                new KeyValuePair<string, object[]>("spritesheet", new object[] { "ChickenFly.png", 4, 2, 97, 72, 0.07 }),
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
            UnitFactory.register("chicken", typeof(Chicken));
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
            Width = 95;
            Height = 70;
            PosX = posX;
            PosY = posY;
            MaxTimeVisable = 7000;
        }
    }
}
