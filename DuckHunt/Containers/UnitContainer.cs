using DuckHunt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt.Containers
{
    public class UnitContainer
    {
        private List<Unit> _units;
        private List<Unit> Units
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

        public int NumUnits { get { return Units.Count; } }

        public void AddUnit(Unit unit)
        {
            Units.Add(unit);
        }

        public void RemoveUnit(Unit unit)
        {
            Units.Remove(unit);
            //UnitsToDelete.Add(unit);
        }

        public void MoveAllUnits()
        {
            foreach(Unit unit in Units)
            {
                unit.Move();
            }
        }

        public void DrawAllUnits()
        {
            foreach(Unit unit in Units)
            {
                unit.Draw();
            }
        }
    }
}
