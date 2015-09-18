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

        public Canvas Canvas { private get; set; }

        private UnitFactory()
        {
            _types = new Dictionary<string, Type>();

            Chicken.RegisterSelf();
        }

        public Unit createUnit(string type)
        {
            // Invoke makes sure this runs on the UI thread as it should. 
            try
            {
                return Application.Current.Dispatcher.Invoke(delegate
                {
                    Unit newUnit = (Unit)Activator.CreateInstance(_types[type]);

                    newUnit.MoveBehavior = new SimpleMoveBehavior();
                    newUnit.DrawBehavior = new SimpleDrawBehavior(Canvas);

                    lock(Locks.UnitContainer)
                    {
                        UnitContainer.AddUnit(newUnit);
                    }

                    return newUnit;
                });
                
            }
            catch
            {
                return null;
            }
        }

        public static void register(string name, Type t)
        {
            _types.Add(name, t);
        }
    }
}
