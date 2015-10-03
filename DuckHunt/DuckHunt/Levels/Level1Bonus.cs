using DuckHunt.Controllers;
using DuckHunt.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt.Levels
{
    class Level1Bonus : BaseLevel
    {
        public override string Name { get { return "Bonus Level!"; } }
        protected override string[] AllowedUnits { get { return new string[] { "chicken" }; } }
        protected override int MaxUnits { get { return 10; } }
        protected override int MaxSpawns { get { return 10; } }
        protected override double SpawnDelay { get { return 0.25; } }

        public Level1Bonus() : base()
        {
        }

        public override void Update(IGame game)
        {
            base.Update(game);

            // Bonus levels stoppen als ze niet perfect zijn.
            // Stop alle nieuwe spawns en eindig het level.
            if (!IsPerfect)
            {
                _totalSpawns = MaxSpawns;
                LevelFactory.Instance.EndLevel(game);
            }
        }

        protected override string GetNextLevel()
        {
            return "level2";
        }
    }
}
