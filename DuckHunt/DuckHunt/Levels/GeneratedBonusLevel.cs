using DuckHunt.Controllers;
using DuckHunt.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt.Levels
{
    public class GeneratedBonusLevel : GeneratedLevel
    {
        public override string Name { get { return "Bonus level!"; } }
        public override string BigText { get { return (_hasStarted ? "" : "Perfecte score!\nBonus level!"); } }

        public override int ShotsLeft { get { return -1; } }

        public GeneratedBonusLevel(int level, string[] allowedUnits, int numUnits, double spawnDelay) : base(level, allowedUnits, numUnits, numUnits, spawnDelay, int.MaxValue)
        {
        }

        public override void Update(IGame game)
        {
            base.Update(game);

            if (_totalMisses > 0)
            {
                EndLevel(game);
            }
        }

        protected override void StartNextLevel(IGame game)
        {
            LevelFactory.Instance.NextLevel(game, _level);
        }
    }
}
