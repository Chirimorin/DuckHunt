using DuckHunt.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt.Controllers
{
    interface IGame
    {
        InputContainer InputContainer { get; }
        UnitContainer UnitContainer { get; }
        Random Random { get; }

        double Time { get; }
        double DT { get; }
        double FPS { get; }
    }
}
