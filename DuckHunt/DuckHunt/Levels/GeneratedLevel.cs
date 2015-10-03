﻿using DuckHunt.Controllers;
using DuckHunt.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt.Levels
{
    public class GeneratedLevel : BaseLevel
    {
        public override string Name { get { return "Level " + _level; } }
        protected override string[] AllowedUnits { get { return _allowedUnits; } }
        protected override int MaxUnits { get { return _maxLivingUnits; } }
        protected override int MaxSpawns { get { return _maxSpawns; } }
        protected override double SpawnDelay { get { return _spawnDelay; } }

        public override int ShotsLeft
        {
            get { return _maxShots - _totalClicks; }
        }

        protected readonly int _level;
        protected readonly string[] _allowedUnits;
        protected readonly int _maxLivingUnits;
        protected readonly int _maxSpawns;
        protected readonly int _maxShots;
        protected readonly double _spawnDelay;

        public GeneratedLevel(int level, string[] allowedUnits, int maxLivingUnits, int maxSpawns, double spawnDelay, int maxShots) : base()
        {
            _level = level;
            _allowedUnits = allowedUnits;
            _maxLivingUnits = maxLivingUnits;
            _maxSpawns = maxSpawns;
            _maxShots = maxShots;
            _spawnDelay = spawnDelay;
        }

        public override void Update(IGame game)
        {
            base.Update(game);

            if (_totalClicks > _maxShots)
            {
                EndLevel(game);
            }
        }

        protected override void StartNextLevel(IGame game)
        {
            // Te veel clicks, game over
            if (_totalClicks > _maxShots)
                LevelFactory.Instance.GameOver(game);
            // Perfecte game, bonus level
            else if (IsPerfect)
                LevelFactory.Instance.BonusLevel(game, _level);
            // Level gehaald, volgende level
            else
                LevelFactory.Instance.NextLevel(game, _level);
        }
    }
}
