using DuckHunt.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DuckHunt.Containers
{
    /// <summary>
    /// Input container. 
    /// </summary>
    public class InputContainer
    {
        private List<Point> _clickedPoints;
        public List<Point> ClickedPoints
        {
            get
            {
                if (_clickedPoints == null)
                {
                    _clickedPoints = new List<Point>();
                }
                return _clickedPoints;
            }
        }

        private Point _mousePosition;
        public Point MousePosition
        {
            get { return _mousePosition; }
            set { _mousePosition = value; }
        }

        public void HandleInputs(IGame game)
        {
            // foreach kan het aanpassen van collections tijdens loopen niet aan. Dit willen we ook niet. 
            lock(Locks.InputContainer)
            {
                foreach (Point point in ClickedPoints)
                {
                    game.UnitContainer.HandleClick(point);
                }
                ClickedPoints.Clear();
            }
        }
    }
}
