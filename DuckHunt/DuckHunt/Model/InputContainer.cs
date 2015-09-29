﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DuckHunt.Model
{
    public class OldInputContainer
    {
        #region Lazy Singleton Implementation
        // The Lazy class guarantees Thread-safe lazy-construction of the object. 
        private static readonly Lazy<OldInputContainer> _instance
            = new Lazy<OldInputContainer>(() => new OldInputContainer());

        private OldInputContainer() { }

        public static OldInputContainer Instance
        {
            get
            {
                // Double-check locking and creation is handled by the Lazy class.
                return _instance.Value;
            }
        }
        #endregion

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
            get
            {
                if (_mousePosition == null)
                    _mousePosition = new Point();
                return _mousePosition;
            }
            set
            {
                _mousePosition = value;
            }
        }
    }
}