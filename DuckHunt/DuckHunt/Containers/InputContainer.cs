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
        // ClickedPoints lijst kan door UI en Game threads worden aangepast, lock voor thread safety.
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
        public int EarnedScore
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
            lock (clickedPointsLock)
            {
                NumClicks = ClickedPoints.Count;
                EarnedScore = 0;
                NumMisses = 0;

                foreach (Point point in ClickedPoints)
                {
                    int score = game.UnitContainer.HandleClick(point);
                    EarnedScore += score;

                    if (score == 0)
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
