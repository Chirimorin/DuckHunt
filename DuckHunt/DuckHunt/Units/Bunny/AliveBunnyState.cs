﻿using DuckHunt.Controllers;
using DuckHunt.Factories;
using DuckHunt.Units.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckHunt.Behaviors.Draw;

namespace DuckHunt.Units.Bunny
{
    public class AliveBunnyState : AliveUnitState
    {
        private IDrawBehavior _runningDrawBehavior;
        private IDrawBehavior _jumpingDrawBehavior;
        
        private bool _isJumping;

        public AliveBunnyState(string name, double fleeTime) : base(name, fleeTime)
        {
            _isJumping = false;
        }

        public AliveBunnyState(string name) : base(name)
        {
            _isJumping = false;
        }

        public override void Update(Unit unit, IGame game)
        {
            IDrawBehavior newBehavior = null;

            if (_isJumping && unit.VY == 0)
            {
                newBehavior = _runningDrawBehavior;
            }
            
            else if (!_isJumping && unit.VY != 0)
            {
                newBehavior = _jumpingDrawBehavior;
            }
            
            if (newBehavior != null)
            {
                _isJumping = !_isJumping;
                UI.TryRemoveGraphics(DrawBehavior.Gfx);
                DrawBehavior = newBehavior;
                UI.TryAddGraphics(DrawBehavior.Gfx);
                DrawBehavior.Reset();
            }

            base.Update(unit, game);
        }

        public override void CreateBehaviors(string unitName)
        {
            // Base stelt de standaard behavior in: running
            base.CreateBehaviors(unitName);

            _runningDrawBehavior = DrawBehavior;
            _jumpingDrawBehavior = UnitFactories.DrawBehaviors.Create(unitName, "jumping");
        }
    }
}
