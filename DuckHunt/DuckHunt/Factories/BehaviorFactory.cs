using DuckHunt.Behaviors.Draw;
using DuckHunt.Behaviors.Move;
using DuckHunt.Behaviors.Move.Chicken;
using DuckHunt.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt.Factories
{
    public static class BehaviorFactory
    {
        public static BaseMoveBehavior createMoveBehavior(string unit, string state)
        {
            Console.WriteLine("Creating move behavior for " + unit + " in state " + state);

            switch (unit)
            {
                case "chicken":
                    switch (state)
                    {
                        case "alive":
                            //return new SimpleMoveBehavior(0, 0);
                            return new AliveChickenMoveBehavior(1000, 1000, 1000, 1000);
                        case "dead":
                            return new AliveChickenMoveBehavior(1000, 1000, 1000, 1000);
                        default:
                            throw new ArgumentException("Onbekende state: " + state, "state");
                    }
                default:
                    throw new ArgumentException("onbekende unit: " + unit, "unit");
            }
        }

        public static IDrawBehavior createDrawBehavior(string unit, string state)
        {
            Console.WriteLine("Creating draw behavior for " + unit + " in state " + state);

            return UI.Invoke<IDrawBehavior>(delegate ()
            {
                Console.WriteLine("Draw behavior invoked");
                switch (unit)
                {
                    case "chicken":
                        switch (state)
                        {
                            case "alive":
                                return new SpriteSheetDrawBehavior("ChickenFly.png", 4, 2, 97, 72, 0.07);
                            case "dead":
                                return new SpriteSheetDrawBehavior("ChickenFly.png", 4, 2, 97, 72, 0.07);
                            default:
                                throw new ArgumentException("Onbekende state: " + state, "state");
                        }
                    default:
                        throw new ArgumentException("onbekende unit: " + unit, "unit");
                }
            });
        }

    }
}
