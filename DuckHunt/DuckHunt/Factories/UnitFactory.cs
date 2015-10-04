using DuckHunt.Controllers;
using DuckHunt.Units;
using DuckHunt.Units.Bunny;
using DuckHunt.Units.Chicken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DuckHunt.Factories
{
    public static class UnitFactory
    {
        private static Random _random;
        private static Random Random {
            get
            {
                if (_random == null)
                    _random = new Random();
                return _random;
            }
        }

        public static Unit createRandomUnit(IGame game, string[] allowedUnits)
        {
            return createUnit(allowedUnits[game.Random.Next(allowedUnits.Length)]);
        }

        public static Unit createUnit(string type)
        {
            Console.WriteLine("Creating unit " + type);

            Point startLocation;

            switch (type)
            {
                case "chicken":
                    startLocation = GetRandomAirStartingPoint(95, 70);
                    return new Chicken(type, 95, 70, startLocation.X, startLocation.Y, GetRandomAirSpeed(), GetRandomAirSpeed());
                case "bunny":
                    startLocation = GetRandomGroundStartingPoint(80, 80);
                    return new Bunny(type, 80, 80, startLocation.X, startLocation.Y, GetRandomGroundSpeed(), 0);
                default:
                    throw new ArgumentException("Onbekende unit: " + type, "type");
            }
        }

        private static Point GetRandomAirStartingPoint(int width, int height)
        {
            Point result = new Point();

            // Kies eerst een beginplek: links, boven of rechts van het scherm
            int location = Random.Next(3);

            if (location == 0 || // Links van het scherm
                location == 2) // Rechts van het scherm
            {
                if (location == 0)
                {
                    result.X = -width;
                }
                else
                {
                    result.X = CONSTANTS.CANVAS_WIDTH;
                }
                
                result.Y = Random.Next(CONSTANTS.CANVAS_HEIGHT) - height;
            }
            else // Boven het scherm
            {
                result.X = Random.Next(-width, CONSTANTS.CANVAS_WIDTH);
                result.Y = -height;
            }

            return result;
        }

        private static Point GetRandomGroundStartingPoint(int width, int height)
        {
            // Willekeurig links of rechts, niet veel te randomizen hier. 
            if (Random.Next(2) == 0)
            {
                return new Point(-width, CONSTANTS.CANVAS_HEIGHT - height);
            }
            else
            {
                return new Point(CONSTANTS.CANVAS_WIDTH, CONSTANTS.CANVAS_HEIGHT - height);
            }
        }

        private static int GetRandomAirSpeed()
        {
            int minSpeed = 750;
            int maxSpeed = 1000;

            return Random.Next(minSpeed, maxSpeed);
        }

        private static int GetRandomGroundSpeed()
        {
            int minSpeed = 400;
            int maxSpeed = 600;

            return Random.Next(minSpeed, maxSpeed);
        }
    }
}
