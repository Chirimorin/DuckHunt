using DuckHunt.Behaviors.Draw;
using DuckHunt.Behaviors.Move;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt.Model
{
    public class Unit
    {
        public IMoveBehavior MoveBehavior { get; set; }
        public IDrawBehavior DrawBehavior { get; set; }
    }
}
