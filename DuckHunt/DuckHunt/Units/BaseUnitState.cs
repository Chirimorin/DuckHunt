using DuckHunt.Behaviors.Draw;
using DuckHunt.Behaviors.Move;
using DuckHunt.Controllers;
using DuckHunt.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DuckHunt.Units
{
    public abstract class BaseUnitState
    {
        #region Properties
        private readonly string _name;
        public string Name
        {
            get { return _name; }
        }

        private IDrawBehavior _drawBehavior;
        protected virtual IDrawBehavior DrawBehavior
        {
            get { return _drawBehavior; }
            set { _drawBehavior = value; }
        }

        private BaseMoveBehavior _moveBehavior;
        protected virtual BaseMoveBehavior MoveBehavior
        {
            get { return _moveBehavior; }
            set { _moveBehavior = value; }
        }
        #endregion

        #region CTOR
        public BaseUnitState(string unit, string name)
        {
            _name = name;

            MoveBehavior = BehaviorFactory.createMoveBehavior(unit, Name);
            DrawBehavior = BehaviorFactory.createDrawBehavior(unit, Name);

            UI.TryAddGraphics(DrawBehavior.Gfx);
        }
        #endregion

        #region Functions
        /// <summary>
        /// Click event
        /// </summary>
        /// <param name="unit">De unit om te controleren</param>
        /// <param name="point">Het punt waar geklikt is</param>
        /// <returns>Het aantal punten opgeleverd door de unit</returns>
        public abstract int onClick(Unit unit, Point point);

        public virtual void Update(Unit unit, IGame game)
        {
            MoveBehavior.Move(unit, game);
        }
        public virtual void FixedTimePassed(Unit unit, IGame game)
        {
            MoveBehavior.FixedTimePassed(unit, game);
        }

        public virtual void Draw(Unit unit, IGame game)
        {
            DrawBehavior.Draw(unit, game);
        }

        public virtual void CleanUp()
        {
            UI.TryRemoveGraphics(DrawBehavior.Gfx);
        }
        #endregion
    }
}
