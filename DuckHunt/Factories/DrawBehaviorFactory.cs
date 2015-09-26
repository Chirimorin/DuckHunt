using DuckHunt.Behaviors.Draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DuckHunt.Factories
{
    public class DrawBehaviorFactory
    {
        #region Singleton Implementation
        private static DrawBehaviorFactory _instance = new DrawBehaviorFactory();

        public static DrawBehaviorFactory Instance
        {
            get
            {
                return _instance;
            }
        }
        #endregion

        private static Dictionary<string, Type> _types;

        private DrawBehaviorFactory()
        {
            _types = new Dictionary<string, Type>();

            SimpleDrawBehavior.RegisterSelf();
            ImageDrawBehavior.RegisterSelf();
            SpriteSheetDrawBehavior.RegisterSelf();
        }

        public BaseDrawBehavior createDrawBehavior(KeyValuePair<string, object[]> type)
        {
            // Invoke makes sure this runs on the UI thread as it should. 
            try
            {
                return Application.Current.Dispatcher.Invoke(delegate
                {
                    BaseDrawBehavior newBehavior;
                    if (_types.ContainsKey(type.Key))
                    {
                        newBehavior = (BaseDrawBehavior)Activator.CreateInstance(_types[type.Key], type.Value);

                        return newBehavior;
                    }

                    return null;
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
