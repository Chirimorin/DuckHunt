using DuckHunt.Units;
using DuckHunt.Units.Chicken;
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
                            return new AliveChickenState(unit, state);
                        case "fleeing":
                            return new FleeingChickenState(unit, state);
                        case "dead":
                            return new DeadChickenState(unit, state);
                        default:
                            throw new ArgumentException("Onbekende state: " + state, "state");
                    }
                default:
                    throw new ArgumentException("onbekende unit: " + unit, "unit");
            }
        }
    }
}
