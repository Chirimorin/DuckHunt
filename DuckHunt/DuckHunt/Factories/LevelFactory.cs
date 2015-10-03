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

        #region Properties
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

        #region Functies
        public void NewGame(IGame game)
        {
            NextLevel(game, "start");
        }
        
        public void NextLevel(IGame game, string newLevel)
        {
            Console.WriteLine("Next level: " + newLevel + "!");
            game.UnitContainer.ClearAllUnits();
            CurrentLevel = CreateLevel(newLevel);
        }

        public void EndLevel(IGame game)
        {
            game.UnitContainer.CleanupUnits();
        }
        #endregion

        #region Level creatie
        public static ILevel CreateLevel(string level)
        {
            switch (level)
            {
                case "start":
                case "level1":
                    return new Level1();
                case "level1bonus":
                    return new Level1Bonus();
                case "level2":
                    return new Level2();
                case "gameover":
                    return new GameOver();
                default:
                    throw new ArgumentException("Onbekend level: " + level, "level");
            }
        }
        #endregion
    }
}
