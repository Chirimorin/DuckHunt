using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DuckHunt.Model
{
    public class DrawHelperContainer
    {
        #region Lazy Singleton Implementation
        // The Lazy class guarantees Thread-safe lazy-construction of the object. 
        private static readonly Lazy<DrawHelperContainer> _instance
            = new Lazy<DrawHelperContainer>(() => new DrawHelperContainer());

        private DrawHelperContainer()
        {
            
        }

        public static DrawHelperContainer Instance
        {
            get
            {
                // Double-check locking and creation is handled by the Lazy class.
                return _instance.Value;
            }
        }
        #endregion

        public Canvas Canvas { get; set; }
        public Label FPS { get; set; }
    }
}
