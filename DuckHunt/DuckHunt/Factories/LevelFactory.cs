using DuckHunt.Controllers;
using DuckHunt.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt.Factories
{
    public class LevelFactory
    {
        #region Lazy singleton
        private static readonly Lazy<LevelFactory> _instance
            = new Lazy<LevelFactory>(() => new LevelFactory());

        // private to prevent direct instantiation.
        private LevelFactory()
        {
        }

        // accessor for instance
        public static LevelFactory Instance
        {
            get { return _instance.Value; }
        }
        #endregion

        #region Current level
        private ILevel _currentLevel;
        /// <summary>
        /// The current level
        /// </summary>
        public ILevel CurrentLevel
        {
            get { return _currentLevel; }
            private set { _currentLevel = value; }
        }
        #endregion

        #region Level management
        /// <summary>
        /// Start een nieuw spel bij level 1.
        /// </summary>
        /// <param name="game">instantie van game</param>
        public void NewGame(IGame game)
        {
            NextLevel(game, 0);
        }
        
        /// <summary>
        /// Start het volgende normale level
        /// </summary>
        /// <param name="game">instantie van Game</param>
        /// <param name="currentLevel">Het huidige level</param>
        public void NextLevel(IGame game, int currentLevel)
        {
            game.UnitContainer.ClearAllUnits(game);
            CurrentLevel = CreateLevel(currentLevel + 1);
        }

        /// <summary>
        /// Start een bonus level voor het huidige level
        /// </summary>
        /// <param name="game">instantie van Game</param>
        /// <param name="currentLevel">Het huidige level</param>
        public void BonusLevel(IGame game, int currentLevel)
        {
            game.UnitContainer.ClearAllUnits(game);
            CurrentLevel = CreateBonusLevel(currentLevel);
        }

        /// <summary>
        /// Eindigt het huidige level, start nog geen nieuwe. 
        /// </summary>
        /// <param name="game">instantie van Game</param>
        public void EndLevel(IGame game)
        {
            game.UnitContainer.CleanupUnits(game);
        }

        /// <summary>
        /// Eindigt het spel en laat het game-over scherm zien.
        /// </summary>
        public void GameOver(IGame game)
        {
            game.UnitContainer.ClearAllUnits(game);
            CurrentLevel = new GameOver();
        }
        #endregion

        #region Level creatie
        private ILevel CreateLevel(int level)
        {
            return new GeneratedLevel(level, 
                GetAllowedUnits(level), 
                GetMaxLivingUnits(level), 
                GetMaxSpawns(level),
                GetSpawnDelay(level), 
                GetMaxShots(level));
        }

        private ILevel CreateBonusLevel(int level)
        {
            return new GeneratedBonusLevel(level, 
                GetAllowedUnits(level), 
                (int)(GetMaxSpawns(level) * 1.5),
                GetSpawnDelay(level) / 2);
        }

        /// <summary>
        /// Functie om de allowedUnits van het huidige level te bepalen.
        /// </summary>
        /// <param name="level">Het huidige level</param>
        /// <returns>Een lijst met units</returns>
        private string[] GetAllowedUnits(int level)
        {
            if (level % 3 == 0) // Elk derde level, beide units
                return new string[] { "chicken", "bunny" };
            if (level % 2 == 1) // Elk oneven level dat geen derde is, alleen chickens
                return new string[] { "chicken" };
            
            // anders alleen bunnys
            return new string[] { "bunny" };
        }

        /// <summary>
        /// Functie om het maximale aantal units van het huidige level te bepalen.
        /// </summary>
        /// <param name="level">het huidige level</param>
        /// <returns>het aantal units.</returns>
        private int GetMaxLivingUnits(int level)
        {
            if (level <= 1)
                return 5;
            if (level <= 3)
                return 4;
            if (level <= 6)
                return 3;
            if (level <= 10)
                return 2;
            return 1;
        }

        /// <summary>
        /// Functie om het totaal aantal enemies per level te bepalen.
        /// </summary>
        /// <param name="level">Het huidige level</param>
        /// <returns>Het aantal units om te spawnen</returns>
        private int GetMaxSpawns(int level)
        {
            return (int)(Math.Floor(Math.Sqrt(level) *2)) + 1;
        }

        /// <summary>
        /// Functie om de delay tussen spawns per level te bepalen.
        /// </summary>
        /// <param name="level">het huidige level</param>
        /// <returns>de delay voor spawnen</returns>
        private double GetSpawnDelay(int level)
        {
            return 1.0/(double)GetMaxLivingUnits(level);
        }

        private int GetMaxShots(int level)
        {
            // We willen minimaal 1 schot per unit, met 3 bonus schoten voor ieder level
            int result = GetMaxSpawns(level) + 3;

            // In eerdere levels krijg je extra schoten
            if (level < 15)
            {
                result += ((15 - level) /2);
            }

            return result;
        }
        #endregion
    }
}
