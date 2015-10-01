using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DuckHunt.Behaviors.OldMove;
using DuckHunt.Controllers;
using DuckHunt.Factories;

namespace DuckHunt.Units.Chicken
{
    public class DeadChickenState : BaseUnitState
    {
        private double Timer { get; set; }
        private double DespawnTime { get; set; }

        public DeadChickenState(string unit, string name) : base(unit, name)
        {
            Timer = 0;
            DespawnTime = 5;
        }

        public override void onClick(Unit unit, Point point)
        {
            // Doe niks
        }

        public override void Update(Unit unit, IGame game)
        {
            base.Update(unit, game);

            Timer += game.DT;

            if (Timer > DespawnTime)
                unit.destroy();
        }
    }
}
