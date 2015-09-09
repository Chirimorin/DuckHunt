using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt.Model
{
    /// <summary>
    /// This class is responsible for keeping all data that needs to be exchanged between threads like input and output. 
    /// </summary>
    class ActionContainer
    {
        #region Lazy Singleton Implementation
        // The Lazy class guarantees Thread-safe lazy-construction of the object. 
        private static readonly Lazy<ActionContainer> _instance
            = new Lazy<ActionContainer>(() => new ActionContainer());

        private ActionContainer() { }

        public static ActionContainer Instance
        {
            get
            {
                // Double-check locking and creation is handled by the Lazy class.
                return _instance.Value;
            }
        }
        #endregion

        // This lock should be used for any changes within the actionContainer to ensure thread safety. 
        private object _lock = new object();

        public double WindowWidth { get; set; }
        public double WindowHeight { get; set; }

        private List<Chicken> _movedObjects = new List<Chicken>();
        public List<Chicken> MovedObjects
        {
            get
            {
                return _movedObjects;
            }
        }


    }
}
