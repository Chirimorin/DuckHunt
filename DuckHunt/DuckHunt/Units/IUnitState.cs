using DuckHunt.Behaviors.Draw;
using DuckHunt.Behaviors.Move;
using DuckHunt.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DuckHunt.Units
{
    public interface IUnitState
    {
        BaseMoveBehavior MoveBehavior { get; set; }
        BaseDrawBehavior DrawBehavior { get; set; }

        void onClick(Unit unit, Point point);

        void Move(Unit unit, IGame game);
        void FixedTimePassed(Unit unit, IGame game);

        void Draw(Unit unit, IGame game);
    }
}
