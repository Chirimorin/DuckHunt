using DuckHunt.Behaviors.Draw;
using DuckHunt.Behaviors.Move;
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
    public static class UnitFactories
    {
        #region Units
        private static readonly Lazy<GenericFactory<string, Unit>> _units
            = new Lazy<GenericFactory<string, Unit>>(() => new GenericFactory<string, Unit>());
        public static GenericFactory<string, Unit> Units
        {
            get { return _units.Value; }
        }
        #endregion

        #region States
        private static readonly Lazy<GenericFactory<string, string, BaseUnitState>> _states
            = new Lazy<GenericFactory<string, string, BaseUnitState>>(() => new GenericFactory<string, string, BaseUnitState>());
        public static GenericFactory<string, string, BaseUnitState> States
        {
            get { return _states.Value; }
        }
        #endregion

        #region MoveBehaviors
        private static readonly Lazy<GenericFactory<string, string, BaseMoveBehavior>> _moveBehaviors
            = new Lazy<GenericFactory<string, string, BaseMoveBehavior>>(() => new GenericFactory<string, string, BaseMoveBehavior>());
        public static GenericFactory<string, string, BaseMoveBehavior> MoveBehaviors
        {
            get { return _moveBehaviors.Value; }
        }
        #endregion

        #region DrawBehaviors
        private static readonly Lazy<GenericFactory<string, string, IDrawBehavior>> _drawBehaviors
            = new Lazy<GenericFactory<string, string, IDrawBehavior>>(() => new GenericFactory<string, string, IDrawBehavior>());
        public static GenericFactory<string, string, IDrawBehavior> DrawBehaviors
        {
            get { return _drawBehaviors.Value; }
        }
        #endregion
    }
}
