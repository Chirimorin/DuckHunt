using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        public DeadUnitState(string name, double despawnTime, double fadeTime) : base(name)
        {
            Timer = 0;
            DespawnTime = despawnTime;
            FadeTime = fadeTime;
        }

        public DeadUnitState(string name) : base(name)
        {
            Timer = 0;
            DespawnTime = 1.50;
            FadeTime = 0.75;
        }

        public override int onClick(Unit unit, Point point)
        {
            // Dode units kunnen geen punten opleveren.
            return 0;
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
