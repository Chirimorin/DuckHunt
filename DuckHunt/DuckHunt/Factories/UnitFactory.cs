using DuckHunt.Controllers;
using DuckHunt.Units;
using DuckHunt.Units.Bunny;
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
        public static Unit createRandomUnit(IGame game, string[] allowedUnits)
        {
            return createUnit(allowedUnits[game.Random.Next(allowedUnits.Length)]);
        }

        public static Unit createUnit(string type)
        {
            Console.WriteLine("Creating unit " + type);
            switch (type)
            {
                case "chicken":
                    return new Chicken(type, 95, 70, -95, 0, 1000, 500);
                case "bunny":
                    return new Bunny(type, 80, 80, -80, CONSTANTS.CANVAS_HEIGHT - 80, 500, 0);
                default:
                    throw new ArgumentException("Onbekende unit: " + type, "type");
            }
        }
    }
}
