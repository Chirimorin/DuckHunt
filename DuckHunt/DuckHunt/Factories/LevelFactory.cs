using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt.Factories
{
    public class LevelFactory
    {
        #region Lazy singleton
        private static readonly Lazy<LevelFactory> _instance
            = new Lazy<LevelFactory>(() => new LevelFactory());

        // private to prevent direct instantiation.
        private LevelFactory()
        {
        }

        // accessor for instance
        public static LevelFactory Instance
        {
            get { return _instance.Value; }
        }
        #endregion

        
    }
}
