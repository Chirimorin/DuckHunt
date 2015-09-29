using DuckHunt.Behaviors.Move;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DuckHunt.Factories
{
    public class MoveBehaviorFactory
    {

        #region Singleton Implementation
        private static MoveBehaviorFactory _instance = new MoveBehaviorFactory();

        public static MoveBehaviorFactory Instance
        {
            get
            {
                return _instance;
            }
        }
        #endregion

        private static Dictionary<string, Type> _types;

        private MoveBehaviorFactory()
        {
            _types = new Dictionary<string, Type>();

            SimpleMoveBehavior.RegisterSelf();
            HorizontalMoveBehavior.RegisterSelf();
            RandomMoveBehavior.RegisterSelf();
            AfraidOfMouseMoveBehavior.RegisterSelf();
            HugMouseMoveBehavior.RegisterSelf();
            GravityMoveBehavior.RegisterSelf();
        }

        public BaseMoveBehavior createMoveBehavior(KeyValuePair<string, object[]> type)
        {
            BaseMoveBehavior newBehavior;
            if (_types.ContainsKey(type.Key))
            {
                newBehavior = (BaseMoveBehavior)Activator.CreateInstance(_types[type.Key], type.Value);

                return newBehavior;
            }

            return null;
        }

        public static void register(string name, Type t)
        {
            _types.Add(name, t);
        }

    }
}
