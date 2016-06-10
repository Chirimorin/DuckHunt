using DuckHunt.Controllers;
using DuckHunt.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DuckHunt.Behaviors.Draw
{
    public interface IDrawBehavior
    {
        void Animate(Unit unit, IGame game);
        void Draw(Unit unit, IGame game);
        void AddGfx(IGame game);
        void RemoveGfx(IGame game);
        void Reset();
    }
}
