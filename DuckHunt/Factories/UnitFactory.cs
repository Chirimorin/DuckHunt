using DuckHunt.Behaviors;
using DuckHunt.Behaviors.Draw;
using DuckHunt.Behaviors.Move;
using DuckHunt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace DuckHunt.Factories
{
    public class UnitFactory
    {
        #region Singleton Implementation
        private static UnitFactory _instance = new UnitFactory();

        public static UnitFactory Instance
        {
            get
            {
                return _instance;
            }
        }
        #endregion

        private static Dictionary<string, Type> _types;

        private UnitFactory()
        {
            _types = new Dictionary<string, Type>();

            Chicken.RegisterSelf();
        }

        public Unit createUnit(string unit)
        {
            Unit newUnit;
            if (_types.ContainsKey(unit))
            {
                newUnit = (Unit)Activator.CreateInstance(_types[unit]);

                newUnit.MoveBehavior = MoveBehaviorFactory.Instance.createMoveBehavior(newUnit.PreferredMoveBehavior);
                newUnit.DrawBehavior = DrawBehaviorFactory.Instance.createDrawBehavior(newUnit.PreferredDrawBehavior);

                newUnit.init(25, 25);

                lock (Locks.UnitContainer)
                {
                    UnitContainer.AddUnit(newUnit);
                }

                return newUnit;
            }

            return null;
        }

        public static void register(string name, Type t)
        {
            _types.Add(name, t);
        }
    }
}
