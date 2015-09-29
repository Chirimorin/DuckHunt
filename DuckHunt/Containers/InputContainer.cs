using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DuckHunt.Containers
{
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
    }
}
