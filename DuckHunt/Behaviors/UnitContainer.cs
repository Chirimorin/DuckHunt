using DuckHunt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DuckHunt.Behaviors
{
    static class UnitContainer
    {
        private static List<Unit> _units;
        private static List<Unit> Units
        {
            get
            {
                if (_units == null)
                {
                    _units = new List<Unit>();
                }
                return _units;
            }
        }

        public static void AddUnit(Unit unit)
        {
            Units.Add(unit);
        }

        public static void RemoveUnit(Unit unit)
        {
            Units.Remove(unit);
        }

        public static void RemoveAllUnits()
        {
            Units.Clear();
        }

        public static void MoveUnits()
        {
            foreach(Unit unit in Units)
            {
                if (unit.MoveBehavior != null)
                {
                    unit.MoveBehavior.Move();
                }
                else
                {
                    Console.WriteLine("Warning! Unit does not have a move behavior!");
                }
            }
        }

        public static void DrawUnits()
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                foreach (Unit unit in Units)
                {
                    if (unit.DrawBehavior != null &&
                        unit.MoveBehavior != null)
                    {
                        unit.DrawBehavior.Draw(unit.MoveBehavior);
                    }
                    else
                    {
                        Console.WriteLine("Warning! Unit does not have a draw behavior!");
                    }
                }
            });
        }
    }
}
