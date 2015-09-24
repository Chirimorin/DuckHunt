﻿using DuckHunt.Behaviors.Draw;
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
                //new KeyValuePair<string, object[]>("simple", null),
                new KeyValuePair<string, object[]>("random", null)
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
                new KeyValuePair<string, object[]>("image", new object[] { "Chicken.png" })
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

        public override void clicked(Point point)
        {
            Console.WriteLine("Click?");
            if (isHit(point))
            {
                Console.WriteLine("click!");
                UnitContainer.RemoveUnit(this);
            }
        }

        public override void init(double width, double height, double posX = 0, double posY = 0)
        {
            Width = 104;
            Height = 90;
            PosX = posX;
            PosY = posY;
        }
    }
}
