using DuckHunt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt.Behaviors.Move
{
    public abstract class BaseMoveBehavior
    {
        public Unit Parent { get; set; }

        public double PosX
        {
            get
            {
                return Parent.PosX;
            }
            set
            {
                Parent.PosX = value;
            }
        }
        public double PosY
        {
            get
            {
                return Parent.PosY;
            }

            set
            {
                Parent.PosY = value;
            }
        }

        public double Width
        {
            get
            {
                return Parent.Width;
            }
        }
        public double Height
        {
            get
            {
                return Parent.Height;
            }
        }

        private double _vX;
        public double VX
        {
            get
            {
                return _vX;
            }
            set
            {
                _vX = value;
            }
        }

        private double _vY;
        public double VY
        {
            get
            {
                return _vY;
            }
            set
            {
                _vY = value;
            }
        }

        public abstract void Move();
    }
}
