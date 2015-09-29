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
            Bunny.RegisterSelf();
        }

        public Unit createRandomUnit()
        {
            if (_types == null ||
                _types.Count == 0)
                return null;

            Random random = new Random();
            return createUnit(_types.ElementAt(random.Next(0, _types.Count)).Key);
        }

        public Unit createUnit(string unit, params object[] args)
        {
            if (_types.ContainsKey(unit))
            {
                Unit newUnit = (Unit)Activator.CreateInstance(_types[unit], args);

                newUnit.MoveBehavior = MoveBehaviorFactory.Instance.createMoveBehavior(newUnit.PreferredMoveBehavior);
                newUnit.DrawBehavior = DrawBehaviorFactory.Instance.createDrawBehavior(newUnit.PreferredDrawBehavior);

                lock (Locks.UnitContainer)
                {
                    OldUnitContainer.AddUnit(newUnit);
                }

                Console.WriteLine("Created unit " + unit);

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
