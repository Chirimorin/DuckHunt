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
        private object clickedPointsLock = new object();

        private List<Point> _clickedPoints;
        public List<Point> ClickedPoints
        {
            get { return _clickedPoints; }
        }

        private Point _mousePosition;
        public Point MousePosition
        {
            get { return _mousePosition; }
            set { _mousePosition = value; }
        }

        private int _numClicks;
        public int NumClicks
        {
            get { return _numClicks; }
            private set { _numClicks = value; }
        }

        private int _numHits;
        public int NumHits
        {
            get { return _numHits; }
            private set { _numHits = value; }
        }

        private int _numMisses;
        public int NumMisses
        {
            get { return _numMisses; }
            private set { _numMisses = value; }
        }



        public InputContainer()
        {
            _clickedPoints = new List<Point>();
        }

        public void HandleInputs(IGame game)
        {
            // foreach kan het aanpassen van collections tijdens loopen niet aan. Dit willen we dus niet. 
            lock (clickedPointsLock)
            {
                NumClicks = ClickedPoints.Count;
                NumHits = 0;
                NumMisses = 0;

                foreach (Point point in ClickedPoints)
                {
                    if (game.UnitContainer.HandleClick(point))
                        NumHits++;
                    else
                        NumMisses++;
                }
                ClickedPoints.Clear();
            }
        }

        public void AddClick(Point point)
        {
            lock (clickedPointsLock)
            {
                ClickedPoints.Add(point);
            }
        }
    }
}
