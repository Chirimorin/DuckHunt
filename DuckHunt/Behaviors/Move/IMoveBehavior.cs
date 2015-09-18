using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt.Behaviors.Move
{
    public interface IMoveBehavior
    {
        int PosX { get; set; }
        int PosY { get; set; }

        int VX { get; set; }
        int VY { get; set; }

        void Move();
    }
}
