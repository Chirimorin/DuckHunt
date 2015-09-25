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

        private static List<Unit> _unitsToDelete;
        private static List<Unit> UnitsToDelete
        {
            get
            {
                if (_unitsToDelete == null)
                {
                    _unitsToDelete = new List<Unit>();
                }
                return _unitsToDelete;
            }
        }

        public static int NumUnits { get { return Units.Count; } }

        public static void AddUnit(Unit unit)
        {
            Units.Add(unit);
        }

        public static void RemoveUnit(Unit unit)
        {
            UnitsToDelete.Add(unit);
        }

        public static void RemoveDeadUnits()
        {
            foreach (Unit unit in UnitsToDelete)
            {
                unit.DrawBehavior.clearGraphics();
                Units.Remove(unit);
            }
            UnitsToDelete.Clear();
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
                    if (unit.DrawBehavior != null)
                    {
                        unit.DrawBehavior.Draw();
                    }
                    else
                    {
                        Console.WriteLine("Warning! Unit does not have a draw behavior!");
                    }
                }
            });
        }

        public static void clicked(Point point)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                foreach (Unit unit in Units)
                {
                    unit.onClick(point);
                }
            });
        }
    }
}
