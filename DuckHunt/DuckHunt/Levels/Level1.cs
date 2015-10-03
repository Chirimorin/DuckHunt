using DuckHunt.Controllers;
using DuckHunt.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt.Levels
{
    public class Level1 : BaseLevel
    {
        public override string Name { get { return "Level 1"; } }
        protected override string[] AllowedUnits { get { return new string[] { "chicken" }; } }
        protected override int MaxUnits { get { return 3; } }
        protected override int MaxSpawns { get { return 7; } }
        protected override double SpawnDelay { get { return 0.5; } }

        public Level1() : base()
        {
        }

        public override void Update(IGame game)
        {
            base.Update(game);
        }

        protected override string GetNextLevel()
        {
            if (IsPerfect)
                return "level1bonus";
            else
                return "level2";
        }
    }
}
