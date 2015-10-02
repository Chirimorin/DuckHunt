using DuckHunt.Units;
using DuckHunt.Units.Chicken;
using DuckHunt.Units.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt.Factories
{
    public static class StateFactory
    {
        public static BaseUnitState createState(string unit, string state)
        {
            Console.WriteLine("Creating state " + state + " for " + unit);

            switch (unit)
            {
                case "chicken":
                    switch (state)
                    {
                        case "alive":
                            return new AliveUnitState(unit, state, 10);
                        case "fleeing":
                            return new FleeingUnitState(unit, state);
                        case "dead":
                            return new DeadUnitState(unit, state, 3.0, 0.5);
                        default:
                            throw new ArgumentException("Onbekende state: " + state, "state");
                    }
                default:
                    throw new ArgumentException("onbekende unit: " + unit, "unit");
            }
        }
    }
}
