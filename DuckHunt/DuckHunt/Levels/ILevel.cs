using DuckHunt.Controllers;
using DuckHunt.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt.Levels
{
    public interface ILevel
    {
        string Name { get; }
        bool ShowText { get; }
        int Kills { get; set; }
        int ShotsLeft { get; }

        Unit TryCreateUnit(IGame game);
        void Update(IGame game);
    }
}
