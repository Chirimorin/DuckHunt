using DuckHunt.Behaviors.Draw;
using DuckHunt.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt.Factories
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

    public class GenericFactory<Tkey1, Tkey2, TClass>
    {
        private readonly GenericFactory<Tuple<Tkey1, Tkey2>, TClass> _factory;

        public GenericFactory()
        {
            _factory = new GenericFactory<Tuple<Tkey1, Tkey2>, TClass>();
        }

        public virtual TClass Create(Tkey1 k1, Tkey2 k2)
        {
            TClass result = _factory.Create(new Tuple<Tkey1, Tkey2>(k1, k2));
            if (result == null || result.Equals(default(TClass)))
                return _factory.Create(new Tuple<Tkey1, Tkey2>(default(Tkey1), k2));
            return result;
        }

        public virtual void Register(Tkey1 k1, Tkey2 k2, Func<TClass> ctor)
        {
            _factory.Register(new Tuple<Tkey1, Tkey2>(k1, k2), ctor);
        }
    }
}
