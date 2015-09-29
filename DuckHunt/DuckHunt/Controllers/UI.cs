using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt.Controllers
{
    class UI
    {
        #region Lazy Singleton Implementation
        // Lazy klasse is thread safe
        private static readonly Lazy<UI> _instance
            = new Lazy<UI>(() => new UI());

        private UI() { }

        public static UI Instance
        {
            get { return _instance.Value; }
        }
        #endregion

        private MainWindow _mainWindow;

        public MainWindow MainWindow
        {
            get { return _mainWindow; }
            set { _mainWindow = value; }
        }


    }
}
