using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DuckHunt.Behaviors.OldMove;
using DuckHunt.Controllers;
using DuckHunt.Factories;

namespace DuckHunt.Units.Common
{
    public class DeadUnitState : BaseUnitState
    {
        private double Timer { get; set; }
        private double DespawnTime { get; set; }
        private double FadeTime { get; set; }

        private double _originalWidth = double.NaN;
        private double OriginalWidth
        {
            get { return _originalWidth; }
            set { _originalWidth = value; }
        }
        private double _originalHeight = double.NaN;
        private double OriginalHeight
        {
            get { return _originalHeight; }
            set { _originalHeight = value; }
        }

        public DeadUnitState(string unit, string name, double despawnTime, double fadeTime) : base(unit, name)
        {
            Timer = 0;
            DespawnTime = despawnTime;
            FadeTime = fadeTime;
        }

        public override void onClick(Unit unit, Point point)
        {
            // Doe niks
        }

        public override void Update(Unit unit, IGame game)
        {
            if (double.IsNaN(OriginalHeight))
            {
                OriginalHeight = unit.Height;
                OriginalWidth = unit.Width;
            }

            base.Update(unit, game);

            Timer += game.DT;

            if (Timer > DespawnTime)
                unit.destroy();
            else if (Timer > (DespawnTime - FadeTime))
            {
                // Tijd dat de unit al aan het faden is
                double sizeRatio = 1 - (((Timer - DespawnTime) + FadeTime) / FadeTime);

                unit.Width = OriginalWidth * sizeRatio;
                unit.Height = OriginalHeight * sizeRatio;
            }
        }
    }
}
