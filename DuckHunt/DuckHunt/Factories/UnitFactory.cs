using DuckHunt.Units;
using DuckHunt.Units.Chicken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt.Factories
{
    public static class UnitFactory
    {
        public static Unit createRandomUnit()
        {
            return createUnit("chicken");
        }

        public static Unit createUnit(string type)
        {
            Console.WriteLine("Creating unit " + type);
            switch (type)
            {
                case "chicken":
                    return new Chicken(type, 95, 70, -95, 0, 200, 200);
                default:
                    throw new ArgumentException("Onbekende unit: " + type, "type");
            }
        }
    }
}
