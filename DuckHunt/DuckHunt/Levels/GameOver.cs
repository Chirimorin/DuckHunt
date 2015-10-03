﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckHunt.Controllers;
using DuckHunt.Units;

namespace DuckHunt.Levels
{
    public class GameOver : ILevel
    {
        public string Name { get { return "Game Over"; } }

        public Unit TryCreateUnit(IGame game)
        {
            // Geen units tijdens game over
            return null;
        }

        public void Update(IGame game)
        {
            // Geen acties tijdens game over (voor nu, new game knop?)
        }
    }
}
