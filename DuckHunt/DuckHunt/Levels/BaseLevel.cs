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
        public abstract int ShotsLeft { get; }
        public int Kills { get; set; }
        public string BigText { get { return (_hasStarted ? "" : Name); } }

        protected abstract string[] AllowedUnits { get; }
        protected abstract int MaxUnits { get; }
        protected abstract int MaxSpawns { get; }
        protected abstract double SpawnDelay { get; }
        protected abstract double TimeBeforeStart { get; }

        protected double _startTime = double.NaN;

        protected double _lastSpawn;
        protected int _totalSpawns;
        protected int _totalClicks;
        protected int _totalHits;
        protected int _totalMisses;

        protected bool _hasEnded;
        protected bool _hasStarted;

        protected virtual bool IsPerfect
        {
            get
            {
                return (_totalMisses == 0 &&
                    Kills == _totalSpawns);
            }
        }

        public BaseLevel()
        {
            Kills = 0;

            _lastSpawn = 0;
            _totalSpawns = 0;
            _totalClicks = 0;
            _totalHits = 0;
            _totalMisses = 0;

            _hasStarted = false;
            _hasEnded = false;
        }


        public Unit TryCreateUnit(IGame game)
        {
            // Spawn niks als het level nog niet begonnen is of als het maximale aantal units gespawned is. 
            if (!_hasStarted || _totalSpawns >= MaxSpawns)
                return null;

            // Geen units op het veld, negeer delay
            if (game.UnitContainer.NumUnits == 0)
                _lastSpawn = 0;

            // Als er units bijmogen en de spawnDelay voorbij is, spawn een nieuwe unit
            if ((game.UnitContainer.NumUnits < MaxUnits) &&
                (game.Time - _lastSpawn > SpawnDelay))
            {
                _lastSpawn = game.Time;
                _totalSpawns++;
                return UnitFactory.createRandomUnit(game, AllowedUnits);
            }

            return null;
        }

        public virtual void Update(IGame game)
        {
            if (!_hasStarted)
            {
                if (double.IsNaN(_startTime))
                    _startTime = game.Time;

                if (game.Time - _startTime > TimeBeforeStart)
                    _hasStarted = true;
            }

            if (LevelIsOngoing())
            {
                _totalClicks += game.InputContainer.NumClicks;
                _totalHits += game.InputContainer.EarnedScore;
                _totalMisses += game.InputContainer.NumMisses;
            }

            if (LevelIsDone(game))
            {
                StartNextLevel(game);
            }
        }

        protected abstract void StartNextLevel(IGame game);

        /// <summary>
        /// Controleert of het level helemaal is afgelopen (klaar om een nieuw level te starten).
        /// </summary>
        /// <param name="game">Instantie van de game controller</param>
        /// <returns>true als het level is afgelopen</returns>
        protected virtual bool LevelIsDone(IGame game)
        {
            // After spawning all units, go to the next level
            return (_totalSpawns >= MaxSpawns &&
                game.UnitContainer.NumUnits == 0);
        }

        /// <summary>
        /// Controleert of het level nog bezig is. Dit kan false zijn voor alle units weg zijn. 
        /// </summary>
        /// <returns>true als het level nog bezig is.</returns>
        protected virtual bool LevelIsOngoing()
        {
            return _hasStarted && !_hasEnded;
        }

        protected virtual void EndLevel(IGame game)
        {
            _hasEnded = true;
            _totalSpawns = MaxSpawns;
            LevelFactory.Instance.EndLevel(game);
        }
    }
}
