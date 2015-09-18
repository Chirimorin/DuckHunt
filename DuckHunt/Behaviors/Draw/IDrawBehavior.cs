using DuckHunt.Behaviors.Move;
using DuckHunt.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DuckHunt.Behaviors.Draw
{
    public interface IDrawBehavior
    {
        void Draw(IMoveBehavior behavior);
    }
}
