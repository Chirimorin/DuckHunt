using DuckHunt.Controllers;
using DuckHunt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DuckHunt.Behaviors.Draw
{
    public abstract class BaseDrawBehavior
    {
        public virtual UIElement Gfx { get; }

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
        public abstract void Draw(IGame game);

        public virtual void clearGraphics()
        {
            TryRemoveGraphics();
        }

        public virtual void TryAddGraphics()
        {
            UI.TryAddGraphics(Gfx);
        }

        public virtual void TryRemoveGraphics()
        {
            UI.TryRemoveGraphics(Gfx);
        }
    }
}
