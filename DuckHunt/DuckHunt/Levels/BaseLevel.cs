using DuckHunt.Controllers;
using DuckHunt.Factories;
using DuckHunt.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt.Levels
{
    public abstract class BaseLevel : ILevel
    {
        public abstract string Name { get; }
        protected abstract string[] AllowedUnits { get; }
        protected abstract int MaxUnits { get; }
        protected abstract int MaxSpawns { get; }
        protected abstract double SpawnDelay { get; }

        protected double _lastSpawn;
        protected int _totalSpawns;
        protected int _totalClicks;
        protected int _totalHits;
        protected int _totalMisses;

        protected bool IsPerfect { get { return _totalMisses == 0; } }

        public BaseLevel()
        {
            _lastSpawn = 0;
            _totalSpawns = 0;
            _totalClicks = 0;
            _totalHits = 0;
            _totalMisses = 0;
        }


        public Unit TryCreateUnit(IGame game)
        {
            if ((game.UnitContainer.NumUnits < MaxUnits) &&
                (game.Time - _lastSpawn > SpawnDelay) &&
                _totalSpawns < MaxSpawns)
            {
                _lastSpawn = game.Time;
                _totalSpawns++;
                return UnitFactory.createRandomUnit(game, AllowedUnits);
            }

            return null;
        }

        public virtual void Update(IGame game)
        {
            _totalClicks += game.InputContainer.NumClicks;
            _totalHits += game.InputContainer.NumHits;
            _totalMisses += game.InputContainer.NumMisses;

            if (LevelIsFinished(game))
            {
                LevelFactory.Instance.NextLevel(game, GetNextLevel());
            }
        }

        protected abstract string GetNextLevel();

        /// <summary>
        /// Controleert of het level is afgelopen. 
        /// </summary>
        /// <param name="game">Instantie van de game controller</param>
        /// <returns>true als het level is afgelopen</returns>
        protected virtual bool LevelIsFinished(IGame game)
        {
            // After spawning all units, go to the next level
            return (_totalSpawns >= MaxSpawns &&
                game.UnitContainer.NumUnits == 0);
        }
    }
}
