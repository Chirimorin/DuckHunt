using DuckHunt.Controllers;
using DuckHunt.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DuckHunt.Behaviors.Draw
{
    public interface IDrawBehavior
    {
        UIElement Gfx { get; }

        void Draw(Unit unit, IGame game);
    }
}
