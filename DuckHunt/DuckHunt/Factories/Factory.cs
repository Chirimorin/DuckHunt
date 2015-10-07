using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt.Factories
{
    public class Factory<T>
    {
        private Factory() { }

        static readonly Dictionary<string, Func<T>> _dict
             = new Dictionary<string, Func<T>>();

        public static T Create(string id)
        {
            Func<T> constructor = null;
            if (_dict.TryGetValue(id, out constructor))
                return constructor();

            throw new ArgumentException("No type registered for this id");
        }

        public static void Register(string id, Func<T> ctor)
        {
            _dict.Add(id, ctor);
        }
    }
}
