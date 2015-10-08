using DuckHunt.Containers;
using DuckHunt.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt.Controllers
{
    public interface IGame
    {
        InputContainer InputContainer { get; }
        UnitContainer UnitContainer { get; }
        GraphicsContainer GraphicsContainer { get; }
        ILevel CurrentLevel { get; set; }
        Random Random { get; }

        double Time { get; }
        double DT { get; }
        double DrawDT { get; }
        double FPS { get; }

        int CurrentScore { get; }

        bool IsRunning { get; }

        void StopGame();
    }
}
