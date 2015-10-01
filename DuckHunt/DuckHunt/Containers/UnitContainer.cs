using DuckHunt.Controllers;
using DuckHunt.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DuckHunt.Containers
{
    /// <summary>
    /// Unit container. Wordt alleen aangepast in the game thread en is dus altijd thread safe. 
    /// </summary>
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

        public void ClearDestroyedUnits()
        {
            foreach (Unit unit in Units.Where(u => u.IsDestroyed))
            {
                //unit.ClearGraphics();
            }

            Units.RemoveAll(u => u.IsDestroyed);
        }

        public void HandleClick(Point point)
        {
            foreach(Unit unit in Units)
            {
                unit.onClick(point);
            }
        }

        public void UpdateAllUnits(IGame game)
        {
            foreach(Unit unit in Units)
            {
                unit.Update(game);
            }
        }

        public void DrawAllUnits(IGame game)
        {
            foreach(Unit unit in Units)
            {
                unit.Draw(game);
            }
        }
        
        public void FixedTimePassed(IGame game)
        {
            foreach(Unit unit in Units)
            {
                unit.FixedTimePassed(game);
            }
        }
    }
}
