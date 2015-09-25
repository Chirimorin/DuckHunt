﻿using DuckHunt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt.Behaviors.Draw
{
    public abstract class BaseDrawBehavior
    {
        private Unit _parent;
        public Unit Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                _parent = value;
                UpdateSize();
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

        public double PosX
        {
            get
            {
                return Parent.PosX;
            }
        }
        public double PosY
        {
            get
            {
                return Parent.PosY;
            }
        }

        public abstract void UpdateSize();
        public abstract void Draw();

        public abstract void clearGraphics();
    }
}
