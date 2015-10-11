using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.factories
{
    public class GenericFactory<TKey, TClass>
    {
        private readonly Dictionary<TKey, Func<TClass>> _dict
             = new Dictionary<TKey, Func<TClass>>();

        public virtual TClass Create(TKey id)
        {
            Func<TClass> constructor = null;
            if (_dict.TryGetValue(id, out constructor))
                return constructor();

            return default(TClass);
        }

        public virtual void Register(TKey id, Func<TClass> ctor)
        {
            _dict.Add(id, ctor);
        }
    }
}
