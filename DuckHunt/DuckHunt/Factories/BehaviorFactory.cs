﻿using DuckHunt.Behaviors.Draw;
using DuckHunt.Behaviors.Move;
using DuckHunt.Behaviors.Move.Running;
using DuckHunt.Behaviors.Move.Flying;
using DuckHunt.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckHunt.Behaviors.Move.Common;

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
                            return new RandomFlightMoveBehavior(900, 900, 750, 750);
                        case "fleeing":
                            return new FlyingFleeMoveBehavior(1000, -100, 750, 500);
                        case "dead":
                            return new DeadUnitMoveBehavior(900, 500);
                        default:
                            throw new ArgumentException("Onbekende state: " + state, "state");
                    }
                case "bunny":
                    switch (state)
                    {
                        case "alive":
                            return new GravityMoveBehavior(0, 900, 500, 500, 400, 0, 100);
                        case "fleeing":
                            return new RunningFleeMoveBehavior(0, 900, 500, 500, 400, 0, 150);
                        case "dead":
                            return new DeadUnitMoveBehavior(900, 500);
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

            return UI.Invoke<IDrawBehavior>(new Func<IDrawBehavior>(() =>
            {
                switch (unit)
                {
                    case "chicken":
                        switch (state)
                        {
                            case "alive":
                            case "fleeing":
                                return new SpriteSheetDrawBehavior("ChickenFly.png", 4, 2, 97, 72, 0.07, 0, true);
                            case "dead":
                                return new SpriteSheetDrawBehavior("ChickenDead.png", 4, 2, 103, 76, 0.07, 12.5, true);
                            default:
                                throw new ArgumentException("Onbekende state: " + state, "state");
                        }
                    case "bunny":
                        switch (state)
                        {
                            case "alive":
                            case "fleeing":
                                return new SpriteSheetDrawBehavior("BunnyRun.png", 4, 1, 40, 40, 0.075, 0, true);
                            case "jumping":
                                return new SpriteSheetDrawBehavior("BunnyJump.png", 6, 1, 40, 40, 0.15, 0, false);
                            case "dead":
                                return new SpriteSheetDrawBehavior("BunnyDead.png", 5, 1, 40, 40, 0.1, 0, false);
                            default:
                                throw new ArgumentException("Onbekende state: " + state, "state");
                        }
                    default:
                        throw new ArgumentException("onbekende unit: " + unit, "unit");
                }
            }));
        }

    }
}
