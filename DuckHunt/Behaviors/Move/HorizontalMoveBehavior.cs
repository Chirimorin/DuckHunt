﻿using DuckHunt.Factories;
using DuckHunt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt.Behaviors.Move
{
    class HorizontalMoveBehavior : BaseMoveBehavior
    {
        public HorizontalMoveBehavior() : base()
        {
            VX = 200;
            VY = 0;
        }

        public static void RegisterSelf()
        {
            MoveBehaviorFactory.register("horizontal", typeof(HorizontalMoveBehavior));
        }

        protected override void Move()
        {
            PosY = ((WindowHeight / 4) * 3);

            baseMoveX();

            EnsureInScreenX(true);
        }
    }
}