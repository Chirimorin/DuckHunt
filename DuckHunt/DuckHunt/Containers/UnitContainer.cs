using DuckHunt.Controllers;
using DuckHunt.Factories;
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
    /// Unit container. Wordt alleen aangepast in the game thread.
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
            Units.RemoveAll(u => u.IsDestroyed);
        }

        /// <summary>
        /// Verwijderd alle units direct
        /// </summary>
        public void ClearAllUnits()
        {
            foreach (Unit unit in Units)
            {
                unit.destroy();
            }
            ClearDestroyedUnits();
        }

        /// <summary>
        /// Haalt alle units netjes weg
        /// </summary>
        public void CleanupUnits()
        {
            foreach (Unit unit in Units)
            {
                if (unit.State.Name != "endlevel")
                    unit.State = StateFactory.createState(unit.Name, "endlevel");
            }
        }

        public int HandleClick(Point point)
        {
            int score = 0;

            foreach (Unit unit in Units)
            {
                score += unit.onClick(point);
            }

            return score;
        }

        public void UpdateAllUnits(IGame game)
        {
            foreach (Unit unit in Units)
            {
                unit.Update(game);
            }
        }

        public void DrawAllUnits(IGame game)
        {
            foreach (Unit unit in Units)
            {
                unit.Draw(game);
            }
        }

        public void FixedTimePassed(IGame game)
        {
            foreach (Unit unit in Units)
            {
                unit.FixedTimePassed(game);
            }
        }
    }
}
