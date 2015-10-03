using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckHunt.Controllers;
using DuckHunt.Factories;

namespace DuckHunt.Levels
{
    public class Level2 : BaseLevel
    {
        public override string Name { get { return "Level 2"; } }
        protected override string[] AllowedUnits { get { return new string[] { "bunny", "chicken" }; } }
        protected override int MaxUnits { get { return 2; } }
        protected override int MaxSpawns { get { return 5; } }
        protected override double SpawnDelay { get { return 1.5; } }

        public Level2() : base()
        {
        }

        public override void Update(IGame game)
        {
            base.Update(game);
        }

        protected override string GetNextLevel()
        {
            return "gameover";
        }
    }
}
