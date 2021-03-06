﻿using DuckHunt.Controllers;
using DuckHunt.Factories;
using DuckHunt.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DuckHunt.Containers
{
    /// <summary>
    /// Unit container. Wordt alleen aangepast in the game thread.
    /// </summary>
    public class UnitContainer
    {
        // UnitList kan worden uitgelezen door UI, lock voor thread safety (zodat de game thread niet aanpast terwijl UI thread loopt)
        private object unitListLock = new object();

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
            // Aanpassing in de list, lock nodig.
            lock (unitListLock)
            {
                Units.Add(unit);
            }
        }

        public void ClearDestroyedUnits()
        {
            // Aanpassing in de list, lock nodig.
            lock (unitListLock)
            {
                Units.RemoveAll(u => u.IsDestroyed);
            }
        }

        /// <summary>
        /// Verwijderd alle units direct
        /// </summary>
        public void ClearAllUnits(IGame game)
        {
            // Lock niet nodig, dit draait op de game thread.
            foreach (Unit unit in Units)
            {
                unit.destroy(game);
            }
        }

        /// <summary>
        /// Haalt alle units netjes weg
        /// </summary>
        public void CleanupUnits(IGame game)
        {
            // Lock niet nodig, dit draait op de game thread.
            foreach (Unit unit in Units)
            {
                if (unit.State.Name != "endlevel")
                    unit.setState(UnitFactories.States.Create(unit.Name, "endlevel"), game);
            }
        }

        public int HandleClick(Point point, IGame game)
        {
            int score = 0;

            // Lock niet nodig, dit draait op de game thread.
            foreach (Unit unit in Units)
            {
                score += unit.onClick(point, game);
            }

            return score;
        }

        public void UpdateAllUnits(IGame game)
        {
            // Lock niet nodig, dit draait op de game thread.
            foreach (Unit unit in Units)
            {
                unit.Update(game);
            }
        }

        public void AnimateAllUnits(IGame game)
        {
            foreach (Unit unit in Units)
            {
                unit.Animate(game);
            }
        }

        public void DrawAllUnits(IGame game)
        {
            // UI thread, dus lock
            lock (unitListLock)
            {
                foreach (Unit unit in Units)
                {
                    unit.Draw(game);
                }
            }
        }

        public void FixedTimePassed(IGame game)
        {
            // Lock niet nodig, dit draait op de game thread.
            foreach (Unit unit in Units)
            {
                unit.FixedTimePassed(game);
            }
        }
    }
}
