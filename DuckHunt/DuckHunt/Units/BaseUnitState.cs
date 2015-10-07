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
using System.Windows.Controls;

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

        private Unit _parent;
        public Unit Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }


        protected IDrawBehavior _drawBehavior;
        protected IDrawBehavior _pendingDrawBehavior;
        protected virtual IDrawBehavior DrawBehavior
        {
            get { return _drawBehavior; }
            set { _pendingDrawBehavior = value; }
        }

        private BaseMoveBehavior _moveBehavior;
        protected virtual BaseMoveBehavior MoveBehavior
        {
            get { return _moveBehavior; }
            set { _moveBehavior = value; }
        }
        #endregion

        #region CTOR
        public BaseUnitState(string name)
        {
            _name = name;
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

        public virtual void Draw(Unit unit, IGame game, Canvas canvas)
        {
            if (_pendingDrawBehavior != null)
            {
                if (DrawBehavior != null)
                    DrawBehavior.RemoveFromCanvas(canvas);
                _drawBehavior = _pendingDrawBehavior;
                _pendingDrawBehavior = null;
                DrawBehavior.AddToCanvas(canvas);
            }

            DrawBehavior.Draw(unit, game);
        }

        public virtual void CleanUp(Canvas canvas)
        {
            DrawBehavior.RemoveFromCanvas(canvas);
        }

        public virtual void CreateBehaviors(string unitName)
        {
            MoveBehavior = UnitFactories.MoveBehaviors.Create(unitName, Name); //BehaviorFactory.createMoveBehavior(unit, Name);
            DrawBehavior = UnitFactories.DrawBehaviors.Create(unitName, Name); //BehaviorFactory.createDrawBehavior(unit, Name);
        }
        #endregion
    }
}
