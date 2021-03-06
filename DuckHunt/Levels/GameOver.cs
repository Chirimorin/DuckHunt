﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckHunt.Controllers;
using DuckHunt.Units;
using DuckHunt.Factories;

namespace DuckHunt.Levels
{
    public class GameOver : ILevel
    {
        public string Name { get { return "Game Over"; } }
        public int Level { get { return -1; } }
        public int ShotsLeft { get { return 0; } }
        public int Kills { get { return 0; } set { } }
        public string BigText { get { return Name + "\nScore: " + _finalScore; } }

        public double SpeedModifier { get { return 1.0; } }

        private int _finalScore = 0;

        public string[] GetAllowedUnits(IGame game)
        {
            // Geen units tijdens game over
            return new string[0];
        }

        public void Update(IGame game)
        {
            game.StopGame();
            _finalScore = game.CurrentScore;
        }
    }
}
